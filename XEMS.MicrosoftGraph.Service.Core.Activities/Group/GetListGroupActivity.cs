using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Graph;
using Serilog.Core;
using XEMS.MicrosoftGraph.Service.Core.Activities.Contracts;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.Group.InputData;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.Group.Model;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.Group.OutputData;

namespace XEMS.MicrosoftGraph.Service.Core.Activities.Group
{
    public class GetListGroupActivity : IRequestActivity<GetListGroupInputData, GetListGroupOutputData>
    {
        private readonly Logger logger;

        public GetListGroupActivity(GraphServiceClient graph, Logger logger)
        {
            Graph = graph;
            this.logger = logger;
        }

        public GraphServiceClient Graph { get; }

        public async Task<GetListGroupOutputData> Execute(GetListGroupInputData request)
        {
            GetListGroupOutputData response = null;

            try
            {
                var AllGroupOnServer = new List<Microsoft.Graph.Group>();
                var groups = await Graph.Groups.Request().Top(998)
                    .GetAsync();

                while (groups.Count > 0)
                {
                    AllGroupOnServer.AddRange(groups);
                    if (groups.NextPageRequest != null)
                        groups = await groups.NextPageRequest
                            .GetAsync();
                    else
                        break;
                }

                response = new GetListGroupOutputData
                {
                    Groups = new List<GroupModel>()
                };

                foreach (var m in AllGroupOnServer)
                    if (Regex.IsMatch(m.DisplayName, @"^[А-ЩЬЮЯҐЄІЇа-щьюяґєії]{0,2}-[0-9]{2}[а-я]?-[1-9]{1}$"))
                        if (m.GroupTypes != null)
                            if (!m.GroupTypes.Contains("Unified"))
                                response.Groups.Add(new GroupModel(m));

                logger.Information("Type: GetListGroupActivity; Method: Execute; Info: Get list group successfully");
            }
            catch (Exception e)
            {
                logger.Error($"Type: GetListGroupActivity; Method: Execute; Error: {e.Message}");
            }

            return await Task.FromResult(response);
        }
    }
}