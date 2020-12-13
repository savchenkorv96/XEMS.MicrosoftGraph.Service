using System;
using System.Threading.Tasks;
using Serilog.Core;
using XEMS.MicrosoftGraph.Service.Core.Activities.Contracts;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.Group.InputData;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.Group.OutputData;
using XEMS.MicrosoftGraph.Service.Core.UseCases.Contracts;

namespace XEMS.MicrosoftGraph.Service.Core.UseCases.Group
{
    public class GetListGroupUseCase : IUseCase<GetListGroupInputData, GetListGroupOutputData>
    {
        private readonly IRequestActivity<GetListGroupInputData, GetListGroupOutputData> Activity;
        private readonly Logger logger;

        public GetListGroupUseCase(IRequestActivity<GetListGroupInputData, GetListGroupOutputData> activity,
            Logger logger)
        {
            Activity = activity;
            this.logger = logger;
        }

        public async Task<GetListGroupOutputData> Execute(GetListGroupInputData request)
        {
            GetListGroupOutputData result = null;

            try
            {
                result = await Activity.Execute(request);
                logger.Information("Type: GetListGroupUseCase; Method: Execute; Info: Get List Group successfully");
            }
            catch (Exception e)
            {
                logger.Error($"Type: GetListGroupUseCase; Method: Execute; Error: {e.Message}");
            }

            return result;
        }
    }
}