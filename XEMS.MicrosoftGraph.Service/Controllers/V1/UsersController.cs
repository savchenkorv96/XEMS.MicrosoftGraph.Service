using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog.Core;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.User.Model;
using XEMS.MicrosoftGraph.Service.Core.Service;

namespace XEMS.MicrosoftGraph.Service.Controllers.V1
{
    /// <summary>
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly Logger logger;
        private readonly UserService userService;

        /// <summary>
        /// </summary>
        public UsersController(UserService userService, Logger logger)
        {
            this.userService = userService;
            this.logger = logger;
        }


        /// <summary>
        ///     Receiving a user by his identifier
        /// </summary>
        /// <param name="GUID"></param>
        /// <returns></returns>
        /// <response code="200">Returns user by identifier</response>
        /// <response code="400">Not exit user by identifier</response>
        /// <response code="401">Not authorized user</response>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Moderator,Administrator")]
        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetUserById(string GUID)
        {
            if (string.IsNullOrEmpty(GUID)) return new BadRequestResult();

            UserModel response;

            try
            {
                response = await userService.GetUserById(GUID);
                logger.Information(
                    $"Type: UsersController; Method: GetUserById; Info: Get User By Id: {GUID} successfully");
            }
            catch (Exception e)
            {
                logger.Error($"Type: UsersController; Method: GetUserById; Error: {e.Message}");
                return new BadRequestObjectResult(e.Message);
            }

            return new OkObjectResult(response);
        }


        /// <summary>
        ///     Receiving a user by his email
        /// </summary>
        /// <param name="Email"></param>
        /// <returns></returns>
        /// <response code="200">Returns user by email</response>
        /// <response code="400">Not exit user by email</response>
        /// <response code="401">Not authorized user</response>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Moderator,Administrator")]
        [HttpGet("GetUserByEmail")]
        public async Task<IActionResult> GetUserByEmail(string Email)
        {
            if (string.IsNullOrEmpty(Email)) return new BadRequestResult();

            UserModel response;

            try
            {
                response = await userService.GetUserByEmail(Email);
                logger.Information(
                    $"Type: UsersController; Method: GetUserByEmail; Info: Get User By Email: {Email} successfully");
            }
            catch (Exception e)
            {
                logger.Error($"Type: UsersController; Method: GetUserByEmail; Error: {e.Message}");
                return new BadRequestObjectResult(e.Message);
            }

            return new OkObjectResult(response);
        }


        /// <summary>
        ///     Update Contact information on teams server
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <response code="200">Return updated user</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Not authorized user</response>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Administrator")]
        [HttpPut("UpdateUserContactInfoById")]
        public async Task<IActionResult> UpdateUserContactInfoById([FromBody] UserModel user)
        {
            if (Equals(user, null)) return new BadRequestResult();

            UserModel response;

            try
            {
                logger.Information(
                    $"Type: UsersController; Method: UpdateUserContactInfoById; Info: Update User Contact Info By Id: {user.GUID} successfully");
                response = await userService.UpdateContactInfo(user);
            }
            catch (Exception e)
            {
                logger.Error($"Type: UsersController; Method: UpdateUserContactInfoById; Error: {e.Message}");
                return new BadRequestObjectResult(e.Message);
            }

            return new OkObjectResult(response);
        }
    }
}