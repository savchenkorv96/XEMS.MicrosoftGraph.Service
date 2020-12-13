using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Serilog.Core;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.Group.InputData;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.Group.Model;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.Group.OutputData;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.User.Model;
using XEMS.MicrosoftGraph.Service.Core.UseCases;
using XEMS.MicrosoftGraph.Service.Core.UseCases.Contracts;

namespace XEMS.MicrosoftGraph.Service.Core.Service
{
    public class GroupService
    {
        private readonly UseCaseFactory _useCaseFactory;
        private readonly Logger logger;

        public GroupService(UseCaseFactory useCaseFactory, Logger logger)
        {
            _useCaseFactory = useCaseFactory;
            this.logger = logger;
        }

        public async Task<GroupModel> CreateGroup(CreateGroupDataModel model)
        {
            try
            {
                var request = new CreateGroupInputData
                {
                    Group = model
                };

                var response = _useCaseFactory.Create<IUseCase<CreateGroupInputData, CreateGroupOutputData>>()
                    .Execute(request);

                logger.Information(
                    $"Type: GroupService; Method: CreateGroup; Info: CreateGroup by Name: {request.Group.GroupName} successfully");

                return response.Result.Group;
            }
            catch (Exception e)
            {
                logger.Error($"Type: GroupService; Method: CreateGroup; Error: {e.Message}");
                throw;
            }
        }

        public async Task<GroupModel> JoinUserToGroupById(JoinUserToGroupDataModel model)
        {
            try
            {
                var request = new JoinUserToGroupByIdInputData
                {
                    JoinUserToGroupDataModel = model
                };

                var response = _useCaseFactory
                    .Create<IUseCase<JoinUserToGroupByIdInputData, JoinUserToGroupByIdOutputData>>()
                    .Execute(request);

                logger.Information(
                    $"Type: GroupService; Method: JoinUserToGroupById; Info: Join User To Group By Id: {request.JoinUserToGroupDataModel.GroupId} successfully");


                return null;
            }
            catch (Exception e)
            {
                logger.Error($"Type: GroupService; Method: JoinUserToGroupById; Error: {e.Message}");
                throw;
            }
        }

        public async Task<GroupModel> GetGroupById(string guid)
        {
            try
            {
                var request = new GetGroupByIdInputData
                {
                    GUID = guid
                };

                var response = _useCaseFactory.Create<IUseCase<GetGroupByIdInputData, GetGroupByIdOutputData>>()
                    .Execute(request);

                logger.Information(
                    $"Type: GroupService; Method: GetGroupById; Info: Get Group By Id: {request.GUID} successfully");

                return response.Result.Group;
            }
            catch (Exception e)
            {
                logger.Error($"Type: GroupService; Method: GetGroupById; Error: {e.Message}");
                throw;
            }
        }


        public async Task<GroupModel> GetGroupByName(string name)
        {
            try
            {
                var request = new GetGroupByNameInputData
                {
                    Name = name
                };

                var response = _useCaseFactory.Create<IUseCase<GetGroupByNameInputData, GetGroupByNameOutputData>>()
                    .Execute(request);

                logger.Information(
                    $"Type: GroupService; Method: GetGroupByName; Info: Get Group By Name: {request.Name} successfully");

                return response.Result.Group;
            }
            catch (Exception e)
            {
                logger.Error($"Type: GroupService; Method: GetGroupByName; Error: {e.Message}");
                throw;
            }
        }

        public async Task<List<GroupModel>> GetListGroup()
        {
            try
            {
                var request = new GetListGroupInputData();

                var response = _useCaseFactory.Create<IUseCase<GetListGroupInputData, GetListGroupOutputData>>()
                    .Execute(request);

                logger.Information("Type: GroupService; Method: GetListGroup; Info: Get List Group successfully");

                return response.Result.Groups;
            }
            catch (Exception e)
            {
                logger.Error($"Type: GroupService; Method: GetListGroup; Error: {e.Message}");
                throw;
            }
        }

        public async Task<List<UserModel>> GetListGroupMemberById(string guid)
        {
            try
            {
                var request = new GetListGroupMemberByIdInputData
                {
                    GUID = guid
                };

                var response = _useCaseFactory
                    .Create<IUseCase<GetListGroupMemberByIdInputData, GetListGroupMemberByIdOutputData>>()
                    .Execute(request);

                logger.Information(
                    $"Type: GroupService; Method: GetListGroupMemberById; Info: Get List Group Member By Id: {request.GUID} successfully");

                return response.Result.Users;
            }
            catch (Exception e)
            {
                logger.Error($"Type: GroupService; Method: GetListGroupMemberById; Error: {e.Message}");
                throw;
            }
        }
    }
}