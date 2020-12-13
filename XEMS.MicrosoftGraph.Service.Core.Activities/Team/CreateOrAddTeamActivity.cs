using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Graph;
using Serilog.Core;
using XEMS.MicrosoftGraph.Service.Core.Activities.Contracts;
using XEMS.MicrosoftGraph.Service.Core.Activities.Helper;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.Team.InputData;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.Team.OutputData;

namespace XEMS.MicrosoftGraph.Service.Core.Activities.Team
{
    public class CreateOrAddTeamActivity : IRequestActivity<CreateOrAddTeamInputData, CreateOrAddTeamOutputData>
    {
        private readonly Logger logger;

        public CreateOrAddTeamActivity(GraphServiceClient graph, Logger logger)
        {
            Graph = graph;
            this.logger = logger;
        }

        public GraphServiceClient Graph { get; }

        public async Task<CreateOrAddTeamOutputData> Execute(CreateOrAddTeamInputData request)
        {
            CreateOrAddTeamOutputData response = null;

            try
            {
                foreach (var item in request.List)
                {
                    var group = new Microsoft.Graph.Group
                    {
                        DisplayName = item.Name,
                        MailEnabled = true,
                        GroupTypes = new List<string>
                        {
                            "Unified"
                        },
                        SecurityEnabled = false,
                        MailNickname = TranslitHelper.Execute(item.Name.Replace(" ", "").Replace(".", "").ToLower())
                    };

                    var result = await Graph.Groups
                        .Request()
                        .AddAsync(group);

                    var owner = new DirectoryObject
                    {
                        Id = item.OwnerGUID
                    };

                    await Graph.Groups[result.Id].Owners.References
                        .Request()
                        .AddAsync(owner);

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

                    var team = await Graph.Groups[result.Id].Team
                        .Request()
                        .PutAsync(teamSettings);


                    var conversationMember = new AadUserConversationMember
                    {
                        AdditionalData = new Dictionary<string, object>()
                    };

                    foreach (var row in item.ParticipantsList)
                        conversationMember.AdditionalData.Add(
                            "user@odata.bind", $"https://graph.microsoft.com/v1.0/users('{row}')");

                    await Graph.Teams[team.Id].Members
                        .Request()
                        .AddAsync(conversationMember);
                }

                response = new CreateOrAddTeamOutputData();

                logger.Information(
                    "Type: CreateOrAddTeamActivity; Method: Execute; Info: Create team for request successfully");
            }
            catch (Exception e)
            {
                logger.Error($"Type: CreateOrAddTeamActivity; Method: Execute; Error: {e.Message}");
            }

            return await Task.FromResult(response);
        }
    }
}