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
    public class
        UpdateContactInfoByIdActivity : IRequestActivity<UpdateContactInfoByIdInputData, UpdateContactInfoByIdOutputData
        >
    {
        private readonly Logger logger;

        public UpdateContactInfoByIdActivity(GraphServiceClient graph, Logger logger)
        {
            Graph = graph;
            this.logger = logger;
        }

        public GraphServiceClient Graph { get; }

        public async Task<UpdateContactInfoByIdOutputData> Execute(UpdateContactInfoByIdInputData request)
        {
            UpdateContactInfoByIdOutputData response = null;
            try
            {
                var changeUser = new Microsoft.Graph.User
                {
                    MobilePhone = request.User.Phone,
                    StreetAddress = request.User.Address
                };

                 await Graph.Users[request.User.GUID].Request()
                    .UpdateAsync(changeUser);

                var result = await Graph.Users[request.User.GUID].Request()
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


                response = new UpdateContactInfoByIdOutputData
                {
                    User = new UserModel(result)
                };

                logger.Information(
                    $"Type: GetUserByIdActivity; Method: Execute; Info: Update info by Id: {request.User.GUID} successfully");
            }
            catch (Exception e)
            {
                logger.Error($"Type: UpdateContactInfoByIdActivity; Method: Execute; Error: {e.Message}");
                throw;
            }

            return await Task.FromResult(response);
        }
    }
}