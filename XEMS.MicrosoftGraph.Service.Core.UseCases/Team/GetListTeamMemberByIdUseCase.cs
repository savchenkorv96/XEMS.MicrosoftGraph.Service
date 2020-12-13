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
        GetListTeamMemberByIdUseCase : IUseCase<GetListTeamMemberByIdInputData, GetListTeamMemberByIdOutputData>
    {
        private readonly IRequestActivity<GetListTeamMemberByIdInputData, GetListTeamMemberByIdOutputData> Activity;
        private readonly Logger logger;

        public GetListTeamMemberByIdUseCase(
            IRequestActivity<GetListTeamMemberByIdInputData, GetListTeamMemberByIdOutputData> activity, Logger logger)
        {
            Activity = activity;
        }

        public async Task<GetListTeamMemberByIdOutputData> Execute(GetListTeamMemberByIdInputData request)
        {
            GetListTeamMemberByIdOutputData result = null;
            try
            {
                result = await Activity.Execute(request);
                logger.Information(
                    $"Type: GetListTeamMemberByIdUseCase; Method: Execute; Info: Get List Team Member By Id: {request.GUID} successfully");
            }
            catch (Exception e)
            {
                logger.Error($"Type: GetListTeamMemberByIdUseCase; Method: Execute; Error: {e.Message}");
            }

            return result;
        }
    }
}