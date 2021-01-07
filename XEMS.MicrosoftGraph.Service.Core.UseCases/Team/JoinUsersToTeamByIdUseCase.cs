using System;
using System.Threading.Tasks;
using Serilog.Core;
using XEMS.MicrosoftGraph.Service.Core.Activities.Contracts;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.Team.InputData;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.Team.OutputData;
using XEMS.MicrosoftGraph.Service.Core.UseCases.Contracts;

namespace XEMS.MicrosoftGraph.Service.Core.UseCases.Team
{
    public class JoinUsersToTeamByIdUseCase : IUseCase<JoinUsersToTeamByIdInputData, JoinUsersToTeamByIdOutputData>
    {
        private readonly IRequestActivity<JoinUsersToTeamByIdInputData, JoinUsersToTeamByIdOutputData> Activity;
        private readonly Logger logger;

        public JoinUsersToTeamByIdUseCase(
            IRequestActivity<JoinUsersToTeamByIdInputData, JoinUsersToTeamByIdOutputData> activity, Logger logger)
        {
            Activity = activity;
            this.logger = logger;
        }

        public async Task<JoinUsersToTeamByIdOutputData> Execute(JoinUsersToTeamByIdInputData request)
        {
            JoinUsersToTeamByIdOutputData result = null;
            try
            {
                result = await Activity.Execute(request);
                logger.Information(
                    $"Type: JoinUsersToTeamByIdUseCase; Method: Execute; Info: Join Users To Team By Id: {request.GUID} successfully");
            }
            catch (Exception e)
            {
                logger.Error($"Type: JoinUsersToTeamByIdUseCase; Method: Execute; Error: {e.Message}");
                throw;
            }

            return result;
        }
    }
}