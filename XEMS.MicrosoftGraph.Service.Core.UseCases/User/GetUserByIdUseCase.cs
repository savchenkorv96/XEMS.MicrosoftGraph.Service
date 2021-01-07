using System;
using System.Threading.Tasks;
using Serilog.Core;
using XEMS.MicrosoftGraph.Service.Core.Activities.Contracts;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.User.InputData;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.User.OutputData;
using XEMS.MicrosoftGraph.Service.Core.UseCases.Contracts;

namespace XEMS.MicrosoftGraph.Service.Core.UseCases.User
{
    public class GetUserByIdUseCase : IUseCase<GetUserByIdInputData, GetUserByIdOutputData>
    {
        private readonly IRequestActivity<GetUserByIdInputData, GetUserByIdOutputData> Activity;
        private readonly Logger logger;

        public GetUserByIdUseCase(IRequestActivity<GetUserByIdInputData, GetUserByIdOutputData> activity, Logger logger)
        {
            Activity = activity;
            this.logger = logger;
        }

        public async Task<GetUserByIdOutputData> Execute(GetUserByIdInputData request)
        {
            GetUserByIdOutputData result = null;
            try
            {
                result = await Activity.Execute(request);
                logger.Information(
                    $"Type: GetUserByIdUseCase; Method: Execute; Info: Get user info by Id: {request.GUID} successfully");
            }
            catch (Exception e)
            {
                logger.Error($"Type: GetUserByIdUseCase; Method: Execute; Error: {e.Message}");
                throw;
            }

            return result;
        }
    }
}