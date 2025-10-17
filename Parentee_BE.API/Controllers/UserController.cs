using Parentee_BE.ActionFilters;
using Parentee_BE.BLL.Services.Interfaces;
using Parentee_BE.DAL.Data.Exceptions;
using Parentee_BE.DAL.Data.Metadatas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Parentee_BE.Constants;
using Parentee_BE.DAL.Data.Enums;
using Parentee_BE.DAL.Data.RequestDTO.Users;

namespace Parentee_BE.API.Controllers;

public class UserController : BaseController<UserController>
{
    #region Create Class Reference
    private readonly IUserService _userService;
    #endregion
    
    #region Constructors
    public UserController (ILogger<UserController> logger, IUserService userService) : base(logger)
    {
        _userService = userService;
    }

    #endregion

    #region Get Method
    [HttpGet(APIEndpointsConstant.UserEndpoints.GET_USER_ENDPOINT)]
    public string GetUser()
    {
        throw new NotFoundException("User not found");
    }
    
    [Authorize]
    [HttpGet(APIEndpointsConstant.UserEndpoints.GET_CURRENT_USER_ENDPOINT)]
    public async Task<IActionResult> GetCurrent()
    {
        return Ok(ApiResponseBuilder.BuildResponse(
                statusCode: StatusCodes.Status201Created,
                isSuccess: true,
                message: "Get current User successfully",
                data:  await _userService.GetCurrentUser()
            )
        );
    }
    
    [Authorize]
    [HttpGet(APIEndpointsConstant.UserEndpoints.GET_MANY_USERS_ENDPOINT)]
    public async Task<IActionResult> GetMany(
        [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10
        )
    {
        return Ok(ApiResponseBuilder.BuildResponse(
                statusCode: StatusCodes.Status201Created,
                isSuccess: true,
                message: "Get many Users successfully",
                data:  await _userService.GetManyUsers(pageSize: pageSize, pageNumber: pageNumber)
            )
        );
    }
    
    [Authorize]
    [HttpGet(APIEndpointsConstant.UserEndpoints.GET_USERS_WITH_NO_FAMILY_ENDPOINT)]
    public async Task<IActionResult> GetUsersWithNoFamily([FromQuery] Gender gender)
    {
        return Ok(ApiResponseBuilder.BuildResponse(
                statusCode: StatusCodes.Status201Created,
                isSuccess: true,
                message: "Get users with no family successfully",
                data:  await _userService.GetUsersWithNoFamily(gender)
            )
        );
    }
    
    [Authorize]
    [HttpGet(APIEndpointsConstant.UserEndpoints.GET_USER_BY_ID_ENDPOINT)]
    public async Task<IActionResult> GetUserById([FromRoute] Guid id)
    {
        return Ok(ApiResponseBuilder.BuildResponse(
                statusCode: StatusCodes.Status201Created,
                isSuccess: true,
                message: "User created successfully",
                data:  await _userService.GetUserById(id)
                )
        );
    }
    #endregion
    
    #region Post Method
    
    [HttpPost(APIEndpointsConstant.UserEndpoints.CREATE_USER_ENDPOINT)]
    [ValidAttributeActionFilter]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequestDTO requestDto)
    {
        return Ok(ApiResponseBuilder.BuildResponse(
            statusCode: StatusCodes.Status201Created,
            isSuccess: true,
            message: "User created successfully!",
            data: await _userService.CreateUser(requestDto)
            )
        );
    }
    
    #endregion
    
    #region Put Method
    [HttpPut(APIEndpointsConstant.UserEndpoints.UPDATE_USER_ENDPOINT)]
    [ValidAttributeActionFilter]
    public async Task<IActionResult> UpdateUser(
        [FromRoute] Guid id,
        [FromBody] UpdateUserRequestDTO user)
    {
        return Ok(ApiResponseBuilder.BuildResponse(
            statusCode: StatusCodes.Status200OK,
            isSuccess: true,
            message: "User updated successfully",
            data: await _userService.UpdateUser(id, user)
            )
        );
    }
    #endregion
    
    #region Delete Method
    [HttpDelete(APIEndpointsConstant.UserEndpoints.DELETE_USER_ENDPOINT)]
    public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
    {
        return Ok(ApiResponseBuilder.BuildResponse(
            statusCode: StatusCodes.Status200OK,
            isSuccess: true,
            message: "User deleted successfully",
            data: await _userService.DeleteUser(id)
            )
        );
    }
    #endregion
}