using System;
using System.Threading.Tasks;
using Serilog.Core;
using XEMS.MicrosoftGraph.Service.Core.Activities.Contracts;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.User.InputData;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.User.OutputData;
using XEMS.MicrosoftGraph.Service.Core.UseCases.Contracts;

namespace XEMS.MicrosoftGraph.Service.Core.UseCases.User
{
    public class
        UpdateContactInfoByIdUseCase : IUseCase<UpdateContactInfoByIdInputData, UpdateContactInfoByIdOutputData>
    {
        private readonly IRequestActivity<UpdateContactInfoByIdInputData, UpdateContactInfoByIdOutputData> Activity;
        private readonly Logger logger;

        public UpdateContactInfoByIdUseCase(
            IRequestActivity<UpdateContactInfoByIdInputData, UpdateContactInfoByIdOutputData> activity, Logger logger)
        {
            Activity = activity;
            this.logger = logger;
        }

        public async Task<UpdateContactInfoByIdOutputData> Execute(UpdateContactInfoByIdInputData request)
        {
            UpdateContactInfoByIdOutputData result = null;
            try
            {
                result = await Activity.Execute(request);
                logger.Information(
                    $"Type: UpdateContactInfoByIdUseCase; Method: Execute; Info: Update Contact Info By Id: {request.User.GUID} successfully");
            }
            catch (Exception e)
            {
                logger.Error($"Type: UpdateContactInfoByIdUseCase; Method: Execute; Error: {e.Message}");
            }

            return result;
        }
    }
}