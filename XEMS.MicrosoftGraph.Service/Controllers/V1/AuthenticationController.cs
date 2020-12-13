using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Serilog.Core;
using XEMS.MicrosoftGraph.Service.Core.Domain.Model.InputData.Auth;
using XEMS.MicrosoftGraph.Service.Core.Helper;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.Auth.InputData;
using XEMS.MicrosoftGraph.Service.Core.Service;

namespace XEMS.MicrosoftGraph.Service.Controllers.V1
{
    /// <summary>
    ///     Authorization controller for working with api
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly IConfiguration _config;
        private readonly Logger logger;

        /// <summary>
        /// </summary>
        /// <param name="config"></param>
        /// <param name="authService"></param>
        public AuthenticationController(IConfiguration config, AuthService authService, Logger logger)
        {
            _config = config;
            _authService = authService;
            this.logger = logger;
        }

        /// <summary>
        ///     Authorization of service by using сredentials
        /// </summary>
        /// <param name="authData"></param>
        /// <returns></returns>
        /// <response code="200">Access token and refresh token issued</response>
        /// <response code="401">Credentials are invalid</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [AllowAnonymous]
        [HttpPost("AuthWithCredentials")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> AuthWithCredentials([FromBody] AuthModelWithCredentials authData)
        {
            IActionResult response = Unauthorized();
            try
            {
                var user = await _authService.AuthenticateUser(authData);

                if (user != null)
                {
                    var tokenString = TokenPublisher.GenerateAccessToken(user, _config);
                    var refreshToken = TokenPublisher.GenerateRefreshToken();

                    user.AccessToken = tokenString;
                    user.RefreshToken = refreshToken;
                    user.RefreshTokenExpiryTime = DateTime.Now.AddDays(365);

                    await _authService.UpdateRefreshToken(new UpdateRefreshTokenModel
                    {
                        Id = user.Id,
                        RefreshToken = user.RefreshToken,
                        RefreshTokenExpiryTime = user.RefreshTokenExpiryTime
                    });

                    response = new OkObjectResult(
                        user
                    );

                    logger.Information(
                        $"Type: AuthenticationController; Method: AuthWithCredentials; Info: Auth With Credentials by Login: {authData.Login} successfully");
                }
            }
            catch (Exception e)
            {
                logger.Error($"Type: AuthenticationController; Method: AuthWithCredentials; Error: {e.Message}");
                throw;
            }

            return response;
        }

        /// <summary>
        ///     Authorization of service by using refreshToken
        /// </summary>
        /// <param name="authData"></param>
        /// <returns>AuthDataModel</returns>
        /// <response code="200">Issued a new access token and refresh token</response>
        /// <response code="402">RefreshToken has been expired</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status402PaymentRequired)]
        [AllowAnonymous]
        [HttpPost("AuthWithRefreshToken")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> AuthModelWithRefreshToken([FromBody] AuthModelWithRefreshToken authData)
        {
            IActionResult response = Unauthorized();
            try
            {
                var user = await _authService.AuthenticateUser(authData);

                if (user != null)
                {
                    if (user.RefreshTokenExpiryTime < DateTime.Now) return new StatusCodeResult(402);

                    var tokenString = TokenPublisher.GenerateAccessToken(user, _config);
                    var refreshToken = TokenPublisher.GenerateRefreshToken();

                    user.AccessToken = tokenString;
                    user.RefreshToken = refreshToken;
                    user.RefreshTokenExpiryTime = DateTime.Now.AddYears(1);

                    await _authService.UpdateRefreshToken(new UpdateRefreshTokenModel
                    {
                        Id = user.Id,
                        RefreshToken = user.RefreshToken,
                        RefreshTokenExpiryTime = user.RefreshTokenExpiryTime
                    });

                    response = new OkObjectResult(
                        user
                    );

                    logger.Information(
                        $"Type: AuthenticationController; Method: AuthModelWithRefreshToken; Info: AuthModelWithRefreshToken by Login: {authData.RefreshToken} successfully");
                }
            }
            catch (Exception e)
            {
                logger.Error($"Type: AuthenticationController; Method: AuthModelWithRefreshToken; Error: {e.Message}");
                throw;
            }

            return response;
        }
    }
}