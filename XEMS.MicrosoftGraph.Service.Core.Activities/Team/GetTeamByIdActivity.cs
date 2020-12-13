using System;
using System.Threading.Tasks;
using Microsoft.Graph;
using Serilog.Core;
using XEMS.MicrosoftGraph.Service.Core.Activities.Contracts;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.Team.InputData;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.Team.Model;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.Team.OutputData;

namespace XEMS.MicrosoftGraph.Service.Core.Activities.Team
{
    public class GetTeamByIdActivity : IRequestActivity<GetTeamByIdInputData, GetTeamByIdOutputData>
    {
        private readonly Logger logger;

        public GetTeamByIdActivity(GraphServiceClient graph, Logger logger)
        {
            Graph = graph;
            this.logger = logger;
        }

        public GraphServiceClient Graph { get; }

        public async Task<GetTeamByIdOutputData> Execute(GetTeamByIdInputData request)
        {
            GetTeamByIdOutputData response = null;

            try
            {
                var teams = await Graph.Teams[request.GUID].Request().GetAsync();

                response = new GetTeamByIdOutputData
                {
                    Team = new TeamModel(teams)
                };

                logger.Information(
                    $"Type: GetTeamByIdActivity; Method: Execute; Info: Get team info by Id: {request.GUID} successfully");
            }
            catch (Exception e)
            {
                logger.Error($"Type: GetTeamByIdActivity; Method: Execute; Error: {e.Message}");
            }

            return await Task.FromResult(response);
        }
    }
}