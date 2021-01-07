using System;
using System.Threading.Tasks;
using Serilog.Core;
using XEMS.MicrosoftGraph.Service.Core.Activities.Contracts;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.Group.InputData;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.Group.OutputData;
using XEMS.MicrosoftGraph.Service.Core.UseCases.Contracts;

namespace XEMS.MicrosoftGraph.Service.Core.UseCases.Group
{
    public class JoinUserToGroupByIdUseCase : IUseCase<JoinUserToGroupByIdInputData, JoinUserToGroupByIdOutputData>
    {
        private readonly IRequestActivity<JoinUserToGroupByIdInputData, JoinUserToGroupByIdOutputData> Activity;
        private readonly Logger logger;

        public JoinUserToGroupByIdUseCase(
            IRequestActivity<JoinUserToGroupByIdInputData, JoinUserToGroupByIdOutputData> activity, Logger logger)
        {
            Activity = activity;
            this.logger = logger;
        }

        public async Task<JoinUserToGroupByIdOutputData> Execute(JoinUserToGroupByIdInputData request)
        {
            JoinUserToGroupByIdOutputData result = null;
            try
            {
                result = await Activity.Execute(request);
                logger.Information(
                    $"Type: JoinUserToGroupByIdUseCase; Method: Execute; Info: Join User To Group By Id: {request.JoinUserToGroupDataModel.GroupId} successfully");
            }
            catch (Exception e)
            {
                logger.Error($"Type: JoinUserToGroupByIdUseCase; Method: Execute; Error: {e.Message}");
                throw;
            }

            return result;
        }
    }
}