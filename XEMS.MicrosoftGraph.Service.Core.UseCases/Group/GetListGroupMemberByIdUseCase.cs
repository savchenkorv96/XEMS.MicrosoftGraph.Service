using System;
using System.Threading.Tasks;
using Serilog.Core;
using XEMS.MicrosoftGraph.Service.Core.Activities.Contracts;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.Group.InputData;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.Group.OutputData;
using XEMS.MicrosoftGraph.Service.Core.UseCases.Contracts;

namespace XEMS.MicrosoftGraph.Service.Core.UseCases.Group
{
    public class
        GetListGroupMemberByIdUseCase : IUseCase<GetListGroupMemberByIdInputData, GetListGroupMemberByIdOutputData>
    {
        private readonly IRequestActivity<GetListGroupMemberByIdInputData, GetListGroupMemberByIdOutputData> Activity;
        private readonly Logger logger;

        public GetListGroupMemberByIdUseCase(
            IRequestActivity<GetListGroupMemberByIdInputData, GetListGroupMemberByIdOutputData> activity, Logger logger)
        {
            Activity = activity;
            this.logger = logger;
        }

        public async Task<GetListGroupMemberByIdOutputData> Execute(GetListGroupMemberByIdInputData request)
        {
            GetListGroupMemberByIdOutputData result = null;
            try
            {
                result = await Activity.Execute(request);
                logger.Information(
                    $"Type: GetListGroupMemberByIdUseCase; Method: Execute; Info: Get List Group Member By Id: {request.GUID} successfully");
            }
            catch (Exception e)
            {
                logger.Error($"Type: GetListGroupMemberByIdUseCase; Method: Execute; Error: {e.Message}");
                throw;
            }

            return result;
        }
    }
}