using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Graph;
using Serilog.Core;
using XEMS.MicrosoftGraph.Service.Core.Activities.Contracts;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.User.InputData;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.User.Model;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.User.OutputData;

namespace XEMS.MicrosoftGraph.Service.Core.Activities.User
{
    public class GetUserByEmailActivity : IRequestActivity<GetUserByEmailInputData, GetUserByEmailOutputData>
    {
        private readonly Logger logger;

        public GetUserByEmailActivity(GraphServiceClient graph, Logger logger)
        {
            Graph = graph;
            this.logger = logger;
        }

        public GraphServiceClient Graph { get; }

        public async Task<GetUserByEmailOutputData> Execute(GetUserByEmailInputData request)
        {
            GetUserByEmailOutputData response = null;
            try
            {
                var singleUser = await Graph.Users.Request()
                    .Filter($"startswith(mail, '{request.Email}')")
                    .GetAsync();

                var user = await Graph.Users[singleUser.First().Id].Request()
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
                response = new GetUserByEmailOutputData
                {
                    User = new UserModel(user)
                };

                logger.Information(
                    $"Type: GetUserByEmailActivity; Method: Execute; Info: Get info by Email: {request.Email} successfully");
            }
            catch (Exception e)
            {
                logger.Error($"Type: GetUserByEmailActivity; Method: Execute; Error: {e.Message}");
                throw;
            }

            return await Task.FromResult(response);
        }
    }
}