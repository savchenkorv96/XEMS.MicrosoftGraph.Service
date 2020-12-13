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
        GetListTeamMemberByIdActivity : IRequestActivity<GetListTeamMemberByIdInputData, GetListTeamMemberByIdOutputData
        >
    {
        private readonly Logger logger;

        public GetListTeamMemberByIdActivity(GraphServiceClient graph, Logger logger)
        {
            Graph = graph;
            this.logger = logger;
        }

        public GraphServiceClient Graph { get; }

        public async Task<GetListTeamMemberByIdOutputData> Execute(GetListTeamMemberByIdInputData request)
        {
            GetListTeamMemberByIdOutputData response = null;

            try
            {
                var memberOf = await Graph.Teams[request.GUID].Members
                    .Request()
                    .GetAsync();

                var users = new List<UserModel>();

                foreach (var item in memberOf)
                {
                    var user = await Graph.Users[item.Id].Request()
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

                response = new GetListTeamMemberByIdOutputData
                {
                    Users = users
                };

                logger.Information(
                    $"Type: GetListTeamMemberByIdActivity; Method: Execute; Info: Get List Team Member By Id: {request.GUID} successfully");
            }
            catch (Exception e)
            {
                logger.Error($"Type: GetListTeamMemberByIdActivity; Method: Execute; Error: {e.Message}");
            }

            return await Task.FromResult(response);
        }
    }
}