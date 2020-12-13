using System;
using System.Threading.Tasks;
using Serilog.Core;
using XEMS.MicrosoftGraph.Service.Core.Activities.Contracts;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.Team.InputData;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.Team.OutputData;
using XEMS.MicrosoftGraph.Service.Core.UseCases.Contracts;

namespace XEMS.MicrosoftGraph.Service.Core.UseCases.Team
{
    public class
        CrateTeamFromGroupByIdUseCase : IUseCase<CrateTeamFromGroupByIdInputData, CrateTeamFromGroupByIdOutputData>
    {
        private readonly IRequestActivity<CrateTeamFromGroupByIdInputData, CrateTeamFromGroupByIdOutputData> Activity;
        private readonly Logger logger;

        public CrateTeamFromGroupByIdUseCase(
            IRequestActivity<CrateTeamFromGroupByIdInputData, CrateTeamFromGroupByIdOutputData> activity, Logger logger)
        {
            Activity = activity;
            this.logger = logger;
        }

        public async Task<CrateTeamFromGroupByIdOutputData> Execute(CrateTeamFromGroupByIdInputData request)
        {
            CrateTeamFromGroupByIdOutputData result = null;
            try
            {
                result = await Activity.Execute(request);
                logger.Information(
                    $"Type: CrateTeamFromGroupByIdUseCase; Method: Execute; Info: Crate Team From Group By Id: {request.GUID} successfully");
            }
            catch (Exception e)
            {
                logger.Error($"Type: CrateTeamFromGroupByIdUseCase; Method: Execute; Error: {e.Message}");
            }

            return result;
        }
    }
}