using System;
using System.Threading.Tasks;
using Serilog.Core;
using XEMS.MicrosoftGraph.Service.Core.Activities.Contracts;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.Group.InputData;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.Group.OutputData;
using XEMS.MicrosoftGraph.Service.Core.UseCases.Contracts;

namespace XEMS.MicrosoftGraph.Service.Core.UseCases.Group
{
    public class CreateGroupUseCase : IUseCase<CreateGroupInputData, CreateGroupOutputData>
    {
        private readonly IRequestActivity<CreateGroupInputData, CreateGroupOutputData> Activity;
        private readonly Logger logger;

        public CreateGroupUseCase(IRequestActivity<CreateGroupInputData, CreateGroupOutputData> activity, Logger logger)
        {
            Activity = activity;
            this.logger = logger;
        }

        public async Task<CreateGroupOutputData> Execute(CreateGroupInputData request)
        {
            CreateGroupOutputData result = null;
            try
            {
                result = await Activity.Execute(request);
                logger.Information("Type: CreateGroupUseCase; Method: Execute; Info: Create group successfully");
            }
            catch (Exception e)
            {
                logger.Error($"Type: CreateGroupUseCase; Method: Execute; Error: {e.Message}");
            }

            return result;
        }
    }
}