using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Serilog.Core;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.Team.InputData;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.Team.Model;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.Team.OutputData;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.User.Model;
using XEMS.MicrosoftGraph.Service.Core.UseCases;
using XEMS.MicrosoftGraph.Service.Core.UseCases.Contracts;

namespace XEMS.MicrosoftGraph.Service.Core.Service
{
    public class TeamService
    {
        private readonly UseCaseFactory _useCaseFactory;
        private readonly Logger logger;

        public TeamService(UseCaseFactory useCaseFactory, Logger logger)
        {
            _useCaseFactory = useCaseFactory;
            this.logger = logger;
        }


        public async Task<object> CreateOrAddTeam(List<CreateTeamOrAddDataModel> model)
        {
            try
            {
                var request = new CreateOrAddTeamInputData
                {
                    List = model
                };

                var response = await _useCaseFactory.Create<IUseCase<CreateOrAddTeamInputData, CreateOrAddTeamOutputData>>()
                    .Execute(request);

                logger.Information("Type: TeamService; Method: CreateOrAddTeam; Info: Create Or Add Team successfully");

                return null;
            }
            catch (Exception e)
            {
                logger.Error($"Type: TeamService; Method: CreateOrAddTeam; Error: {e.Message}");
                throw;
            }
        }

        public async Task<TeamModel> CrateTeamFromGroupById(string guid)
        {
            try
            {
                var request = new CrateTeamFromGroupByIdInputData
                {
                    GUID = guid
                };

                var response = await _useCaseFactory
                    .Create<IUseCase<CrateTeamFromGroupByIdInputData, CrateTeamFromGroupByIdOutputData>>()
                    .Execute(request);

                logger.Information(
                    $"Type: TeamService; Method: CrateTeamFromGroupById; Info: Crate Team From Group By Id {request.GUID} successfully");

                return response.Team;
            }
            catch (Exception e)
            {
                logger.Error($"Type: TeamService; Method: CrateTeamFromGroupById; Error: {e.Message}");
                throw;
            }
        }

        public async Task<List<UserModel>> GetListTeamMemberById(string guid)
        {
            try
            {
                var request = new GetListTeamMemberByIdInputData
                {
                    GUID = guid
                };

                var response = await _useCaseFactory
                    .Create<IUseCase<GetListTeamMemberByIdInputData, GetListTeamMemberByIdOutputData>>()
                    .Execute(request);

                logger.Information(
                    $"Type: TeamService; Method: GetListTeamMemberById; Info: Get List Team Member By Id {request.GUID} successfully");

                return response.Users;
            }
            catch (Exception e)
            {
                logger.Error($"Type: TeamService; Method: GetListTeamMemberById; Error: {e.Message}");
                throw;
            }
        }

        public async Task<List<UserModel>> JoinUsersToTeamById(string guid, List<string> users)
        {
            try
            {
                var request = new JoinUsersToTeamByIdInputData
                {
                    GUID = guid,
                    UsersGUID = users
                };

                var response = await _useCaseFactory
                    .Create<IUseCase<JoinUsersToTeamByIdInputData, JoinUsersToTeamByIdOutputData>>()
                    .Execute(request);

                logger.Information(
                    $"Type: TeamService; Method: JoinUsersToTeamById; Info: Join Users To Team By Id {request.GUID} successfully");

                return response.Users;
            }
            catch (Exception e)
            {
                logger.Error($"Type: TeamService; Method: JoinUsersToTeamById; Error: {e.Message}");
                throw;
            }
        }

        public async Task<TeamModel> GetTeamById(string guid)
        {
            try
            {
                var request = new GetTeamByIdInputData
                {
                    GUID = guid
                };

                var response = await _useCaseFactory.Create<IUseCase<GetTeamByIdInputData, GetTeamByIdOutputData>>()
                    .Execute(request);

                logger.Information(
                    $"Type: TeamService; Method: GetTeamById; Info: Get Team By Id {request.GUID} successfully");

                return response.Team;
            }
            catch (Exception e)
            {
                logger.Error($"Type: TeamService; Method: GetTeamById; Error: {e.Message}");
                throw;
            }
        }
    }
}