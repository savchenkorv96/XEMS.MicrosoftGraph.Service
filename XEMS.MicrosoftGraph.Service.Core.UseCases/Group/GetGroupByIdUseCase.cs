using System;
using System.Threading.Tasks;
using Serilog.Core;
using XEMS.MicrosoftGraph.Service.Core.Activities.Contracts;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.Group.InputData;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.Group.OutputData;
using XEMS.MicrosoftGraph.Service.Core.UseCases.Contracts;

namespace XEMS.MicrosoftGraph.Service.Core.UseCases.Group
{
    public class GetGroupByIdUseCase : IUseCase<GetGroupByIdInputData, GetGroupByIdOutputData>
    {
        private readonly IRequestActivity<GetGroupByIdInputData, GetGroupByIdOutputData> Activity;
        private readonly Logger logger;

        public GetGroupByIdUseCase(IRequestActivity<GetGroupByIdInputData, GetGroupByIdOutputData> activity,
            Logger logger)
        {
            Activity = activity;
            this.logger = logger;
        }

        public async Task<GetGroupByIdOutputData> Execute(GetGroupByIdInputData request)
        {
            GetGroupByIdOutputData result = null;
            try
            {
                result = await Activity.Execute(request);
                logger.Information(
                    $"Type: GetGroupByIdUseCase; Method: Execute; Info: Get group info by Id: {request.GUID} successfully");
            }
            catch (Exception e)
            {
                logger.Error($"Type: GetGroupByIdUseCase; Method: Execute; Error: {e.Message}");
            }

            return result;
        }
    }
}