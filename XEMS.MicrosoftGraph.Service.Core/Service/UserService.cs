using System;
using System.Threading.Tasks;
using Serilog.Core;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.User.InputData;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.User.Model;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.User.OutputData;
using XEMS.MicrosoftGraph.Service.Core.UseCases;
using XEMS.MicrosoftGraph.Service.Core.UseCases.Contracts;

namespace XEMS.MicrosoftGraph.Service.Core.Service
{
    public class UserService
    {
        private readonly UseCaseFactory _useCaseFactory;
        private readonly Logger logger;

        public UserService(UseCaseFactory useCaseFactory, Logger logger)
        {
            _useCaseFactory = useCaseFactory;
            this.logger = logger;
        }

        public async Task<UserModel> GetUserById(string guid)
        {
            try
            {
                var request = new GetUserByIdInputData
                {
                    GUID = guid
                };

                var response = _useCaseFactory.Create<IUseCase<GetUserByIdInputData, GetUserByIdOutputData>>()
                    .Execute(request);

                logger.Information(
                    $"Type: UserService; Method: GetUserById; Info: Get user info by Id: {request.GUID} successfully");

                return response.Result.User;
            }
            catch (Exception e)
            {
                logger.Error($"Type: UserService; Method: GetUserById; Error: {e.Message}");
                throw;
            }
        }

        public async Task<UserModel> GetUserByEmail(string email)
        {
            try
            {
                var request = new GetUserByEmailInputData
                {
                    Email = email
                };

                var response = await _useCaseFactory.Create<IUseCase<GetUserByEmailInputData, GetUserByEmailOutputData>>()
                    .Execute(request);

                logger.Information(
                    $"Type: UserService; Method: GetUserByEmail; Info:Get User By Email: {request.Email} successfully");

                return response.User;
            }
            catch (Exception e)
            {
                logger.Error($"Type: UserService; Method: GetUserByEmail; Error: {e.Message}");
                throw;
            }
        }

        public async Task<UserModel> UpdateContactInfo(UserModel user)
        {
            try
            {
                var request = new UpdateContactInfoByIdInputData
                {
                    User = user
                };

                var response = await _useCaseFactory
                    .Create<IUseCase<UpdateContactInfoByIdInputData, UpdateContactInfoByIdOutputData>>()
                    .Execute(request);

                logger.Information(
                    $"Type: UserService; Method: UpdateContactInfo; Info: Update Contact Info By Id: {request.User.GUID} successfully");

                return response.User;
            }
            catch (Exception e)
            {
                logger.Error($"Type: UserService; Method: UpdateContactInfo; Error: {e.Message}");
                throw;
            }
        }
    }
}