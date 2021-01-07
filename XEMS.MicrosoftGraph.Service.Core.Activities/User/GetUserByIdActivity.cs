using System;
using System.Threading.Tasks;
using Microsoft.Graph;
using Serilog.Core;
using XEMS.MicrosoftGraph.Service.Core.Activities.Contracts;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.User.InputData;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.User.Model;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.User.OutputData;

namespace XEMS.MicrosoftGraph.Service.Core.Activities.User
{
    public class GetUserByIdActivity : IRequestActivity<GetUserByIdInputData, GetUserByIdOutputData>
    {
        private readonly Logger logger;

        public GetUserByIdActivity(GraphServiceClient graph, Logger logger)
        {
            Graph = graph;
            this.logger = logger;
        }

        public GraphServiceClient Graph { get; }

        public async Task<GetUserByIdOutputData> Execute(GetUserByIdInputData request)
        {
            GetUserByIdOutputData response = null;
            try
            {
                var user = await Graph.Users[request.GUID].Request()
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

                response = new GetUserByIdOutputData
                {
                    User = new UserModel(user)
                };

                logger.Information(
                    $"Type: GetUserByIdActivity; Method: Execute; Info: Get info by Id: {request.GUID} successfully");
            }
            catch (Exception e)
            {
                logger.Error($"Type: GetUserByIdActivity; Method: Execute; Error: {e.Message}");
                throw;
            }

            return await Task.FromResult(response);
        }
    }
}