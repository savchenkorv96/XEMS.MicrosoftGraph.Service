using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog.Core;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.Team.Model;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.User.Model;
using XEMS.MicrosoftGraph.Service.Core.Service;

namespace XEMS.MicrosoftGraph.Service.Controllers.V1
{
    //[Authorize]
    /// <summary>
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    public class TeamsController : ControllerBase
    {
        private readonly Logger logger;
        private readonly TeamService teamService;

        /// <summary>
        /// </summary>
        public TeamsController(TeamService teamService, Logger logger)
        {
            this.teamService = teamService;
            this.logger = logger;
        }

        /// <summary>
        ///     Building a team and filling it with all the information it needs
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        /// <response code="200">Team creation completed successfully</response>
        /// <response code="400">Team creation unsuccessfully</response>
        /// <response code="401">Not authorized user</response>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Administrator")]
        [HttpPost("CreateOrAddTeam")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> CreateOrAddTeam(List<CreateTeamOrAddDataModel> list)
        {
            if (Equals(list, null)) return new BadRequestResult();

            try
            {
                await teamService.CreateOrAddTeam(list);
                logger.Information(
                    "Type: TeamsController; Method: CreateOrAddTeam; Info: Create Or Add Team successfully");
            }
            catch (Exception e)
            {
                logger.Error($"Type: TeamsController; Method: CreateOrAddTeam; Error: {e.Message}");
                return new BadRequestObjectResult(e.Message);
            }

            return new OkResult();
        }

        /// <summary>
        ///     Creating a team based on an existing team
        /// </summary>
        /// <param name="GUID"></param>
        /// <returns></returns>
        /// <response code="200">Creation of team based on group completed successfully</response>
        /// <response code="400">Creation of team based on group failed</response>
        /// <response code="401">Not authorized user</response>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Administrator")]
        [HttpPost("CrateTeamFromGroupById")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> CrateTeamFromGroupById(string GUID)
        {
            if (string.IsNullOrEmpty(GUID)) return new BadRequestResult();

            TeamModel response;

            try
            {
                response = await teamService.CrateTeamFromGroupById(GUID);
                logger.Information(
                    $"Type: TeamsController; Method: Execute; Info: Crate Team From Group By Id: {GUID} successfully");
            }
            catch (Exception e)
            {
                logger.Error($"Type: TeamsController; Method: CrateTeamFromGroupById; Error: {e.Message}");
                return new BadRequestObjectResult(e.Message);
            }

            return new OkObjectResult(response);
        }


        /// <summary>
        ///     Adding users to the corresponding command
        /// </summary>
        /// <param name="GUID"></param>
        /// <param name="users"></param>
        /// <returns></returns>
        /// <response code="200">Added users to the command successfully</response>
        /// <response code="400">Adding users to the command failed</response>
        /// <response code="401">Not authorized user</response>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Administrator")]
        [HttpPost("JoinUsersToTeamById")]
        [MapToApiVersion("1.0")]
        public async Task<object> JoinUsersToTeamById(string GUID, List<string> users)
        {
            if (string.IsNullOrEmpty(GUID)) return new BadRequestResult();

            List<UserModel> response;

            try
            {
                response = await teamService.JoinUsersToTeamById(GUID, users);
                logger.Information(
                    $"Type: TeamsController; Method: JoinUsersToTeamById; Info: Join Users To Team By Id: {GUID} successfully");
            }
            catch (Exception e)
            {
                logger.Error($"Type: TeamsController; Method: JoinUsersToTeamById; Error: {e.Message}");
                return new BadRequestObjectResult(e.Message);
            }

            return new OkObjectResult(response);
        }

        /// <summary>
        ///     Getting a group by ID
        /// </summary>
        /// <param name="GUID"></param>
        /// <returns></returns>
        /// <response code="200">Group was found</response>
        /// <response code="400">Group was not found</response>
        /// <response code="401">Not authorized user</response>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Moderator,Administrator")]
        [HttpGet("GetTeamById")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> GetTeamById(string GUID)
        {
            if (string.IsNullOrEmpty(GUID)) return new BadRequestResult();

            TeamModel response;

            try
            {
                response = await teamService.GetTeamById(GUID);
                logger.Information(
                    $"Type: TeamsController; Method: GetTeamById; Info: Get Team By Id: {GUID} successfully");
            }
            catch (Exception e)
            {
                logger.Error($"Type: TeamsController; Method: GetTeamById; Error: {e.Message}");
                return new BadRequestObjectResult(e.Message);
            }

            return new OkObjectResult(response);
        }


        /// <summary>
        ///     Getting a list of the user of the requested team
        /// </summary>
        /// <param name="GUID"></param>
        /// <returns></returns>
        /// <response code="200">Group was found</response>
        /// <response code="400">Group was not found</response>
        /// <response code="401">Not authorized user</response>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Moderator,Administrator")]
        [HttpGet("GetListTeamMemberById")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> GetListTeamMemberById(string GUID)
        {
            if (string.IsNullOrEmpty(GUID)) return new BadRequestResult();

            List<UserModel> response;

            try
            {
                response = await teamService.GetListTeamMemberById(GUID);
                logger.Information(
                    $"Type: TeamsController; Method: GetListTeamMemberById; Info: Get List Team Member By Id: {GUID} successfully");
            }
            catch (Exception e)
            {
                logger.Error($"Type: TeamsController; Method: GetListTeamMemberById; Error: {e.Message}");
                return new BadRequestObjectResult(e.Message);
            }

            return new OkObjectResult(response);
        }
    }
}