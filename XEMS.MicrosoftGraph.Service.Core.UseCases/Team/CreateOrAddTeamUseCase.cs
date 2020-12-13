using System;
using System.Threading.Tasks;
using XEMS.MicrosoftGraph.Service.Core.Activities.Contracts;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.Team.InputData;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.Team.OutputData;
using XEMS.MicrosoftGraph.Service.Core.UseCases.Contracts;

namespace XEMS.MicrosoftGraph.Service.Core.UseCases.Team
{
    public class CreateOrAddTeamUseCase : IUseCase<CreateOrAddTeamInputData, CreateOrAddTeamOutputData>
    {
        private readonly IRequestActivity<CreateOrAddTeamInputData, CreateOrAddTeamOutputData> Activity;

        public CreateOrAddTeamUseCase(IRequestActivity<CreateOrAddTeamInputData, CreateOrAddTeamOutputData> activity)
        {
            Activity = activity;
        }

        public async Task<CreateOrAddTeamOutputData> Execute(CreateOrAddTeamInputData request)
        {
            try
            {
                return await Activity.Execute(request);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}