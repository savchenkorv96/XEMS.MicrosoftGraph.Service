using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Graph;
using Serilog.Core;
using XEMS.MicrosoftGraph.Service.Core.Activities.Contracts;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.Team.InputData;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.Team.OutputData;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.User.Model;

namespace XEMS.MicrosoftGraph.Service.Core.Activities.Team
{
    public class
        JoinUsersToTeamByIdActivity : IRequestActivity<JoinUsersToTeamByIdInputData, JoinUsersToTeamByIdOutputData>
    {
        private readonly Logger logger;

        public JoinUsersToTeamByIdActivity(GraphServiceClient graph, Logger logger)
        {
            Graph = graph;
            this.logger = logger;
        }

        public GraphServiceClient Graph { get; }

        public async Task<JoinUsersToTeamByIdOutputData> Execute(JoinUsersToTeamByIdInputData request)
        {
            JoinUsersToTeamByIdOutputData response = null;

            try
            {
                var conversationMember = new AadUserConversationMember
                {
                    AdditionalData = new Dictionary<string, object>()
                };

                foreach (var row in request.UsersGUID)
                    conversationMember.AdditionalData.Add(
                        "user@odata.bind", $"https://graph.microsoft.com/v1.0/users('{row}')");

                await Graph.Teams[request.GUID].Members
                    .Request()
                    .AddAsync(conversationMember);

                var memberOf = await Graph.Teams[request.GUID].Members
                    .Request()
                    .GetAsync();

                var users = new List<UserModel>();

                foreach (var cm in memberOf)
                {
                    var item = (AadUserConversationMember)cm;
                    var user = await Graph.Users[item.UserId].Request()
                        .Select(u => new
                        {
                            u.Id,
                            u.DisplayName,
                            u.Department,
                            u.Mail,
                            u.MailNickname,
                            u.MobilePhone,
                            u.Birthday,
                            u.CreatedDateTime,
                            u.JobTitle,
                            u.StreetAddress
                        })
                        .GetAsync();

                    var userModel = new UserModel(user);

                    users.Add(userModel);
                }

                response = new JoinUsersToTeamByIdOutputData
                {
                    Users = users
                };

                logger.Information(
                    $"Type: JoinUsersToTeamByIdActivity; Method: Execute; Info: Join Users To Team By Id: {request.GUID} successfully");
            }
            catch (Exception e)
            {
                logger.Error($"Type: JoinUsersToTeamByIdActivity; Method: Execute; Error: {e.Message}");
                throw;
            }

            return await Task.FromResult(response);
        }
    }
}