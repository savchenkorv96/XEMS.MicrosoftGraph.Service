using System;
using System.Threading.Tasks;
using Serilog.Core;
using XEMS.MicrosoftGraph.Service.Core.Domain.Model.InputData.Auth;
using XEMS.MicrosoftGraph.Service.Core.Mapper;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.Auth.InputData;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.Auth.OutputData;
using XEMS.MicrosoftGraph.Service.DataAccess.Domain.Entity;
using XEMS.MicrosoftGraph.Service.DataAccess.Repository;

namespace XEMS.MicrosoftGraph.Service.Core.Service
{
    public class AuthService
    {
        private readonly AuthServiceRepository _authServiceRepository;

        private readonly Logger logger;

        public AuthService(AuthServiceRepository authServiceRepository, Logger logger)
        {
            _authServiceRepository = authServiceRepository;
            this.logger = logger;
        }

        public async Task<AuthResultModel> AuthenticateUser(AuthModelWithCredentials data)
        {
            AuthResultModel result = null;
            try
            {
                var authData = await _authServiceRepository.GetUserByСredentials(data.Login, data.Password);
                result = MapperExtensions.Convert<AuthData, AuthResultModel>(authData);

                logger.Information($"AuthenticateUser [WithCredentials]  by {data.Login} was successfully");
            }
            catch (Exception e)
            {
                logger.Error($"Method: AuthenticateUser With Credentials Message: {e.Message}");
                throw;
            }

            return result;
        }

        public async Task<AuthResultModel> AuthenticateUser(AuthModelWithRefreshToken data)
        {
            AuthResultModel result = null;

            try
            {
                var authData = await _authServiceRepository.GetUserByRefreshToken(data.RefreshToken);
                result = MapperExtensions.Convert<AuthData, AuthResultModel>(authData);
                logger.Information($"AuthenticateUser [RefreshToken ]  by {data.RefreshToken} was successfully");
            }
            catch (Exception e)
            {
                logger.Error($"Method: AuthenticateUser with RefreshToken  Message: {e.Message}");
                throw;
            }

            return result;
        }

        public async Task<AuthResultModel> UpdateRefreshToken(UpdateRefreshTokenModel model)
        {
            AuthResultModel result = null;

            try
            {
                await _authServiceRepository.UpdateRefreshToken(model.Id, model.RefreshToken,
                    model.RefreshTokenExpiryTime);
                var authData = await _authServiceRepository.GetUserByRefreshToken(model.RefreshToken);
                result = MapperExtensions.Convert<AuthData, AuthResultModel>(authData);

                logger.Information($"UpdateRefreshToken for user id: {model.Id} was successfully");
            }
            catch (Exception e)
            {
                logger.Error($"Method: UpdateRefreshToken Message: {e.Message}");
                throw;
            }

            return result;
        }
    }
}