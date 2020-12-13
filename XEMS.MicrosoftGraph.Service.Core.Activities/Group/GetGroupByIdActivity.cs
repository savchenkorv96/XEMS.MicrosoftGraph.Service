using System;
using System.Threading.Tasks;
using Microsoft.Graph;
using Serilog.Core;
using XEMS.MicrosoftGraph.Service.Core.Activities.Contracts;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.Group.InputData;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.Group.Model;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.Group.OutputData;

namespace XEMS.MicrosoftGraph.Service.Core.Activities.Group
{
    public class GetGroupByIdActivity : IRequestActivity<GetGroupByIdInputData, GetGroupByIdOutputData>
    {
        private readonly Logger logger;

        public GetGroupByIdActivity(GraphServiceClient graph, Logger logger)
        {
            Graph = graph;
            this.logger = logger;
        }

        public GraphServiceClient Graph { get; }

        public async Task<GetGroupByIdOutputData> Execute(GetGroupByIdInputData request)
        {
            GetGroupByIdOutputData response = null;

            try
            {
                var group = await Graph.Groups[request.GUID].Request().GetAsync();

                response = new GetGroupByIdOutputData
                {
                    Group = new GroupModel(group)
                };

                logger.Information(
                    $"Type: GetGroupByIdActivity; Method: Execute; Info: Get group info by Id: {request.GUID} successfully");
            }
            catch (Exception e)
            {
                logger.Error($"Type: GetGroupByIdActivity; Method: Execute; Error: {e.Message}");
            }

            return await Task.FromResult(response);
        }
    }
}