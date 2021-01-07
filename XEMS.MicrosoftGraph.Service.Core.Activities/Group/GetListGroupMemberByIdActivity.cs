using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Graph;
using Serilog.Core;
using XEMS.MicrosoftGraph.Service.Core.Activities.Contracts;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.Group.InputData;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.Group.OutputData;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.User.Model;

namespace XEMS.MicrosoftGraph.Service.Core.Activities.Group
{
    public class
        GetListGroupMemberByIdActivity : IRequestActivity<GetListGroupMemberByIdInputData,
            GetListGroupMemberByIdOutputData>
    {
        private readonly Logger logger;

        public GetListGroupMemberByIdActivity(GraphServiceClient graph, Logger logger)
        {
            Graph = graph;
            this.logger = logger;
        }

        public GraphServiceClient Graph { get; }

        public async Task<GetListGroupMemberByIdOutputData> Execute(GetListGroupMemberByIdInputData request)
        {
            GetListGroupMemberByIdOutputData response = null;

            try
            {
                var memberOf = await Graph.Groups[request.GUID].Members
                    .Request()
                    .GetAsync();

                var users = new List<UserModel>();

                foreach (Microsoft.Graph.User item in memberOf)
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

                response = new GetListGroupMemberByIdOutputData
                {
                    Users = users
                };

                logger.Information(
                    $"Type: GetListGroupMemberByIdActivity; Method: Execute; Info: Get List Group Member By Id: {request.GUID} successfully");
            }
            catch (Exception e)
            {
                logger.Error($"Type: GetListGroupMemberByIdActivity; Method: Execute; Error: {e.Message}");
                throw;
            }

            return await Task.FromResult(response);
        }
    }
}