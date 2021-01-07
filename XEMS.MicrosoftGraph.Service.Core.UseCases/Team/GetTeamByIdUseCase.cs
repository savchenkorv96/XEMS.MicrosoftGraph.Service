using System;
using System.Threading.Tasks;
using Serilog.Core;
using XEMS.MicrosoftGraph.Service.Core.Activities.Contracts;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.Team.InputData;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.Team.OutputData;
using XEMS.MicrosoftGraph.Service.Core.UseCases.Contracts;

namespace XEMS.MicrosoftGraph.Service.Core.UseCases.Team
{
    public class GetTeamByIdUseCase : IUseCase<GetTeamByIdInputData, GetTeamByIdOutputData>
    {
        private readonly IRequestActivity<GetTeamByIdInputData, GetTeamByIdOutputData> Activity;
        private readonly Logger logger;

        public GetTeamByIdUseCase(IRequestActivity<GetTeamByIdInputData, GetTeamByIdOutputData> activity, Logger logger)
        {
            Activity = activity;
            this.logger = logger;
        }

        public async Task<GetTeamByIdOutputData> Execute(GetTeamByIdInputData request)
        {
            GetTeamByIdOutputData result = null;
            try
            {
                result = await Activity.Execute(request);
                logger.Information(
                    $"Type: GetTeamByIdUseCase; Method: Execute; Info: Get Team By Id: {request.GUID} successfully");
            }
            catch (Exception e)
            {
                logger.Error($"Type: GetTeamByIdUseCase; Method: Execute; Error: {e.Message}");
                throw;
            }

            return result;
        }
    }
}