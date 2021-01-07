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
    public class
        CrateTeamFromGroupByIdActivity : IRequestActivity<CrateTeamFromGroupByIdInputData,
            CrateTeamFromGroupByIdOutputData>
    {
        private readonly Logger logger;

        public CrateTeamFromGroupByIdActivity(GraphServiceClient graph, Logger logger)
        {
            Graph = graph;
            this.logger = logger;
        }

        public GraphServiceClient Graph { get; }

        public async Task<CrateTeamFromGroupByIdOutputData> Execute(CrateTeamFromGroupByIdInputData request)
        {
            CrateTeamFromGroupByIdOutputData response = null;

            try
            {
                var teamSettings = new Microsoft.Graph.Team
                {
                    MemberSettings = new TeamMemberSettings
                    {
                        AllowCreatePrivateChannels = true,
                        AllowCreateUpdateChannels = true,
                        ODataType = null
                    },
                    MessagingSettings = new TeamMessagingSettings
                    {
                        AllowUserEditMessages = true,
                        AllowUserDeleteMessages = true,
                        ODataType = null
                    },
                    FunSettings = new TeamFunSettings
                    {
                        AllowGiphy = true,
                        GiphyContentRating = GiphyRatingType.Strict,
                        ODataType = null
                    },
                    ODataType = null
                };

                var team = await Graph.Groups[request.GUID].Team
                    .Request()
                    .PutAsync(teamSettings);

                response = new CrateTeamFromGroupByIdOutputData
                {
                    Team = new TeamModel(team)
                };

                logger.Information(
                    $"Type: CrateTeamFromGroupByIdActivity; Method: Execute; Info: Crate Team From Group By Id: {request.GUID} successfully");
            }
            catch (Exception e)
            {
                logger.Error($"Type: CrateTeamFromGroupByIdActivity; Method: Execute; Error: {e.Message}");
                throw;
            }

            return await Task.FromResult(response);
        }
    }
}