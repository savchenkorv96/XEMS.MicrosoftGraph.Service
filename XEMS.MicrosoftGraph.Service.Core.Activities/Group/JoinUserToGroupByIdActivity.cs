using System;
using System.Threading.Tasks;
using Microsoft.Graph;
using Serilog.Core;
using XEMS.MicrosoftGraph.Service.Core.Activities.Contracts;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.Group.InputData;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.Group.OutputData;

namespace XEMS.MicrosoftGraph.Service.Core.Activities.Group
{
    public class
        JoinUserToGroupByIdActivity : IRequestActivity<JoinUserToGroupByIdInputData, JoinUserToGroupByIdOutputData>
    {
        private readonly Logger logger;

        public JoinUserToGroupByIdActivity(GraphServiceClient graph, Logger logger)
        {
            Graph = graph;
            this.logger = logger;
        }

        public GraphServiceClient Graph { get; }

        public async Task<JoinUserToGroupByIdOutputData> Execute(JoinUserToGroupByIdInputData request)
        {
            JoinUserToGroupByIdOutputData response = null;

            try
            {
                foreach (var user in request.JoinUserToGroupDataModel.UsersId)
                {
                    var directoryObject = new DirectoryObject
                    {
                        Id = user
                    };

                    await Graph.Groups[request.JoinUserToGroupDataModel.GroupId].Members.References
                        .Request()
                        .AddAsync(directoryObject);
                }

                response = new JoinUserToGroupByIdOutputData();

                logger.Information(
                    $"Type: JoinUserToGroupByIdActivity; Method: Execute; Info: Join User To Group By Id: {request.JoinUserToGroupDataModel.GroupId} | User count: {request.JoinUserToGroupDataModel.UsersId.Count} successfully");
            }
            catch (Exception e)
            {
                logger.Error($"Type: JoinUserToGroupByIdActivity; Method: Execute; Error: {e.Message}");
            }

            return await Task.FromResult(response);
        }
    }
}