using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Graph;
using Serilog.Core;
using XEMS.MicrosoftGraph.Service.Core.Activities.Contracts;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.Group.InputData;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.Group.Model;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.Group.OutputData;

namespace XEMS.MicrosoftGraph.Service.Core.Activities.Group
{
    public class GetGroupByNameActivity : IRequestActivity<GetGroupByNameInputData, GetGroupByNameOutputData>
    {
        private readonly Logger logger;

        public GetGroupByNameActivity(GraphServiceClient graph, Logger logger)
        {
            Graph = graph;
            this.logger = logger;
        }

        public GraphServiceClient Graph { get; }

        public async Task<GetGroupByNameOutputData> Execute(GetGroupByNameInputData request)
        {
            GetGroupByNameOutputData response = null;

            try
            {
                var groups = await Graph.Groups.Request()
                    .Filter($"startswith(displayName, '{request.Name}')")
                    .GetAsync();

                var group = await Graph.Groups[groups.First().Id].Request().GetAsync();

                response = new GetGroupByNameOutputData
                {
                    Group = new GroupModel(group)
                };

                logger.Information(
                    $"Type: GetGroupByNameActivity; Method: Execute; Info: Get group info by Name: {request.Name} successfully");
            }
            catch (Exception e)
            {
                logger.Error($"Type: GetGroupByNameActivity; Method: Execute; Error: {e.Message}");
            }

            return response;
        }
    }
}