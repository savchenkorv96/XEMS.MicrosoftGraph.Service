using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Graph;
using Serilog.Core;
using XEMS.MicrosoftGraph.Service.Core.Activities.Contracts;
using XEMS.MicrosoftGraph.Service.Core.Activities.Helper;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.Group.InputData;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.Group.Model;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.Group.OutputData;

namespace XEMS.MicrosoftGraph.Service.Core.Activities.Group
{
    public class CreateGroupActivity : IRequestActivity<CreateGroupInputData, CreateGroupOutputData>
    {
        private readonly Logger logger;

        public CreateGroupActivity(GraphServiceClient graph, Logger logger)
        {
            Graph = graph;
            this.logger = logger;
        }

        public GraphServiceClient Graph { get; }

        public async Task<CreateGroupOutputData> Execute(CreateGroupInputData request)
        {
            CreateGroupOutputData response = null;

            try
            {
                var group = new Microsoft.Graph.Group
                {
                    DisplayName = request.Group.GroupName,
                    MailEnabled = true,
                    GroupTypes = new List<string>
                    {
                        "Unified"
                    },
                    SecurityEnabled = false,
                    MailNickname = TranslitHelper.Execute(request.Group.GroupName.ToLower())
                };

                var result = await Graph.Groups
                    .Request()
                    .AddAsync(group);

                response = new CreateGroupOutputData
                {
                    Group = new GroupModel(result)
                };

                logger.Information(
                    $"Type: CreateGroupActivity; Method: Execute; Info: Create group with name: {request.Group.GroupName} successfully");
            }
            catch (Exception e)
            {
                logger.Error($"Type: CreateGroupActivity; Method: Execute; Error: {e.Message}");
                throw;
            }

            return await Task.FromResult(response);
        }
    }
}