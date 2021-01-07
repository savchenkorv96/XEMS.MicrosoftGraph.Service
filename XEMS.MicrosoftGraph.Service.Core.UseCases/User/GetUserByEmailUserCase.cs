using System;
using System.Threading.Tasks;
using Serilog.Core;
using XEMS.MicrosoftGraph.Service.Core.Activities.Contracts;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.User.InputData;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.User.OutputData;
using XEMS.MicrosoftGraph.Service.Core.UseCases.Contracts;

namespace XEMS.MicrosoftGraph.Service.Core.UseCases.User
{
    public class GetUserByEmailUserCase : IUseCase<GetUserByEmailInputData, GetUserByEmailOutputData>
    {
        private readonly IRequestActivity<GetUserByEmailInputData, GetUserByEmailOutputData> Activity;
        private readonly Logger logger;

        public GetUserByEmailUserCase(IRequestActivity<GetUserByEmailInputData, GetUserByEmailOutputData> activity,
            Logger logger)
        {
            Activity = activity;
            this.logger = logger;
        }

        public async Task<GetUserByEmailOutputData> Execute(GetUserByEmailInputData request)
        {
            GetUserByEmailOutputData result = null;
            try
            {
                result = await Activity.Execute(request);
                logger.Information(
                    $"Type: GetUserByEmailUserCase; Method: Execute; Info: Get user info by Email: {request.Email} successfully");
            }
            catch (Exception e)
            {
                logger.Error($"Type: GetUserByEmailUserCase; Method: Execute; Error: {e.Message}");
                throw;
            }

            return result;
        }
    }
}