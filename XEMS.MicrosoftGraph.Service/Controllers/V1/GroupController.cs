using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog.Core;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.Group.Model;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.User.Model;
using XEMS.MicrosoftGraph.Service.Core.Service;

namespace XEMS.MicrosoftGraph.Service.Controllers.V1
{
    /// <summary>
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    public class GroupController : ControllerBase
    {
        private readonly GroupService groupService;
        private readonly Logger logger;

        /// <summary>
        /// </summary>
        public GroupController(GroupService groupService, Logger logger)
        {
            this.groupService = groupService;
            this.logger = logger;
        }

        /// <summary>
        ///     Create Group
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        /// <response code="200">Group creation completed successfully</response>
        /// <response code="400">Group creation not successful</response>
        /// <response code="401">Not authorized user</response>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Administrator")]
        [HttpPost("CreateGroup")]
        [MapToApiVersion("1.0")]
        public async Task<object> CreateGroup(CreateGroupDataModel group)
        {
            if (Equals(group, null)) return new BadRequestResult();

            GroupModel response;

            try
            {
                response = await groupService.CreateGroup(group);
                logger.Information("Type: GroupController; Method: CreateGroup; Info: Create Group successfully");
            }
            catch (Exception e)
            {
                logger.Error($"Type: GroupController; Method: CreateGroup; Error: {e.Message}");
                return new BadRequestObjectResult(e.Message);
            }

            return new OkObjectResult(response);
        }

        /// <summary>
        ///     Join User To Group
        /// </summary>
        /// <param name="joinUsers"></param>
        /// <returns></returns>
        /// <response code="200">Adding a user to a group completed successfully</response>
        /// <response code="400">Adding a user to a group not successful</response>
        /// <response code="401">Not authorized user</response>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Administrator")]
        [HttpPost("JoinUserToGroupById")]
        [MapToApiVersion("1.0")]
        public async Task<object> JoinUserToGroupById(JoinUserToGroupDataModel joinUsers)
        {
            if (Equals(joinUsers, null)) return new BadRequestResult();

            GroupModel response;

            try
            {
                response = await groupService.JoinUserToGroupById(joinUsers);
                logger.Information(
                    $"Type: GroupController; Method: JoinUserToGroupById; Info: Join User To Group By Id {joinUsers.GroupId} successfully");
            }
            catch (Exception e)
            {
                logger.Error($"Type: GroupController; Method: JoinUserToGroupById; Error: {e.Message}");
                return new BadRequestObjectResult(e.Message);
            }

            return new OkResult();
        }

        /// <summary>
        ///     Return user by id
        /// </summary>
        /// <param name="GUID"></param>
        /// <returns></returns>
        /// <response code="200">Returns a group by id</response>
        /// <response code="400">The group was not found by the specified id</response>
        /// <response code="401">Not authorized user</response>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Moderator,Administrator")]
        [HttpGet("GetGroupById")]
        [MapToApiVersion("1.0")]
        public async Task<object> GetGroupById(string GUID)
        {
            if (string.IsNullOrEmpty(GUID)) return new BadRequestResult();

            GroupModel response;

            try
            {
                response = await groupService.GetGroupById(GUID);
                logger.Information(
                    $"Type: GroupController; Method: JoinUserToGroupById; Info: Join User To Group By Id: {GUID} successfully");
            }
            catch (Exception e)
            {
                logger.Error($"Type: GroupController; Method: JoinUserToGroupById; Error: {e.Message}");
                return new BadRequestObjectResult(e.Message);
            }

            return new OkObjectResult(response);
        }


        /// <summary>
        ///     Return user by name
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        /// <response code="200">Returns a group by name</response>
        /// <response code="400">The group was not found by the specified name</response>
        /// <response code="401">Not authorized user</response>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Moderator,Administrator")]
        [HttpGet("GetGroupByName")]
        [MapToApiVersion("1.0")]
        public async Task<object> GetGroupByName(string groupName)
        {
            if (string.IsNullOrEmpty(groupName)) return new BadRequestResult();

            GroupModel response;

            try
            {
                response = await groupService.GetGroupByName(groupName);
                logger.Information(
                    $"Type: GroupController; Method: GetGroupByName; Info: Get Group By Name: {groupName} successfully");
            }
            catch (Exception e)
            {
                logger.Error($"Type: GroupController; Method: GetGroupByName; Error: {e.Message}");
                return new BadRequestObjectResult(e.Message);
            }

            return new OkObjectResult(response);
        }

        /// <summary>
        ///     List of groups
        /// </summary>
        /// <returns></returns>
        /// <response code="200">The list of the group's fully selected</response>
        /// <response code="400">The list of the selected group cannot be displayed</response>
        /// <response code="401">Not authorized user</response>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Moderator,Administrator")]
        [HttpGet("GetListGroup")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> GetListGroup()
        {
            List<GroupModel> response;

            try
            {
                response = await groupService.GetListGroup();
                logger.Information("Type: GroupController; Method: GetListGroup; Info: Get List Group successfully");
            }
            catch (Exception e)
            {
                logger.Error($"Type: GroupController; Method: GetListGroup; Error: {e.Message}");
                return new BadRequestObjectResult(e.Message);
            }

            return new OkObjectResult(response);
        }

        /// <summary>
        ///     Getting a list of users by group ID
        /// </summary>
        /// <param name="GUID"></param>
        /// <returns></returns>
        /// <response code="200">Returns a list of users by the requested group</response>
        /// <response code="400">Can't find users by group id</response>
        /// <response code="401">Not authorized user</response>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Moderator,Administrator")]
        [HttpGet("GetListGroupMemberById")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> GetListGroupMemberById(string GUID)
        {
            if (string.IsNullOrEmpty(GUID)) return new BadRequestResult();

            List<UserModel> response;

            try
            {
                response = await groupService.GetListGroupMemberById(GUID);
                logger.Information(
                    $"Type: GetUserByIdActivity; Method: GetListGroupMemberById; Info: Get List Group Member By Id: {GUID} successfully");
            }
            catch (Exception e)
            {
                logger.Error($"Type: GroupController; Method: GetListGroupMemberById; Error: {e.Message}");
                return new BadRequestObjectResult(e.Message);
            }

            return new OkObjectResult(response);
        }
    }
}