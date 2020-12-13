using System;
using System.Threading.Tasks;
using Serilog.Core;
using XEMS.MicrosoftGraph.Service.Core.Activities.Contracts;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.Group.InputData;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.Group.OutputData;
using XEMS.MicrosoftGraph.Service.Core.UseCases.Contracts;

namespace XEMS.MicrosoftGraph.Service.Core.UseCases.Group
{
    public class GetGroupByNameUseCase : IUseCase<GetGroupByNameInputData, GetGroupByNameOutputData>
    {
        private readonly IRequestActivity<GetGroupByNameInputData, GetGroupByNameOutputData> Activity;
        private readonly Logger logger;

        public GetGroupByNameUseCase(IRequestActivity<GetGroupByNameInputData, GetGroupByNameOutputData> activity,
            Logger logger)
        {
            Activity = activity;
            this.logger = logger;
        }

        public async Task<GetGroupByNameOutputData> Execute(GetGroupByNameInputData request)
        {
            GetGroupByNameOutputData result = null;
            try
            {
                result = await Activity.Execute(request);
                logger.Information(
                    $"Type: GetGroupByNameUseCase; Method: Execute; Info: Get group info by Name: {request.Name} successfully");
            }
            catch (Exception e)
            {
                logger.Error($"Type: GetGroupByNameUseCase; Method: Execute; Error: {e.Message}");
            }

            return result;
        }
    }
}