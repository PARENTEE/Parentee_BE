using Parentee_BE.ActionFilters;
using Parentee_BE.BLL.Services.Interfaces;
using Parentee_BE.Constants;
using Parentee_BE.DAL.Data.Exceptions;
using Parentee_BE.DAL.Data.Metadatas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Parentee_BE.DAL.Data.RequestDTO.Users;

namespace Parentee_BE.Controllers;

public class UserController : BaseController<UserController>
{
    #region Create Class Reference
    private readonly IUserService _userService;
    #endregion
    
    #region Constructors
    public UserController (ILogger<UserController> logger, IUserService accountService) : base(logger)
    {
        _userService = accountService;
    }

    #endregion

    #region Get Method
    [HttpGet(APIEndpointsConstant.AccountEndpoints.GET_ACCOUNT_ENDPOINT)]
    public string GetAccount()
    {
        throw new NotFoundException("Account not found");
    }
    
    [Authorize]
    [HttpGet(APIEndpointsConstant.AccountEndpoints.GET_CURRENT_ACCOUNT_ENDPOINT)]
    public async Task<IActionResult> GetCurrent()
    {
        return Ok(ApiResponseBuilder.BuildResponse(
                statusCode: StatusCodes.Status201Created,
                isSuccess: true,
                message: "Get current account successfully",
                data:  await _userService.GetCurrentUser()
            )
        );
    }
    
    [Authorize]
    [HttpGet(APIEndpointsConstant.AccountEndpoints.GET_MANY_ACCOUNTS_ENDPOINT)]
    public async Task<IActionResult> GetMany(
        [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10
        )
    {
        return Ok(ApiResponseBuilder.BuildResponse(
                statusCode: StatusCodes.Status201Created,
                isSuccess: true,
                message: "Get many accounts successfully",
                data:  await _userService.GetManyUsers(pageSize: pageSize, pageNumber: pageNumber)
            )
        );
    }
    
    [Authorize]
    [HttpGet(APIEndpointsConstant.AccountEndpoints.GET_ACCOUNT_BY_ID_ENDPOINT)]
    public async Task<IActionResult> GetAccountById([FromRoute] Guid id)
    {
        return Ok(ApiResponseBuilder.BuildResponse(
                statusCode: StatusCodes.Status201Created,
                isSuccess: true,
                message: "Account created successfully",
                data:  await _userService.GetUserById(id)
                )
        );
    }
    #endregion
    
    #region Post Method
    
    [HttpPost(APIEndpointsConstant.AccountEndpoints.CREATE_ACCOUNT_ENDPOINT)]
    [ValidAttributeActionFilter]
    public async Task<IActionResult> CreateAccount([FromBody] CreateUserRequestDTO requestDto)
    {
        return Ok(ApiResponseBuilder.BuildResponse(
            statusCode: StatusCodes.Status201Created,
            isSuccess: true,
            message: "Account created successfully",
            data: await _userService.CreateUser(requestDto)
            )
        );
    }
    
    #endregion
    
    #region Put Method
    [HttpPut(APIEndpointsConstant.AccountEndpoints.UPDATE_ACCOUNT_ENDPOINT)]
    [ValidAttributeActionFilter]
    public async Task<IActionResult> UpdateAccount(
        [FromRoute] Guid id,
        [FromBody] UpdateUserRequestDTO user)
    {
        return Ok(ApiResponseBuilder.BuildResponse(
            statusCode: StatusCodes.Status200OK,
            isSuccess: true,
            message: "Account updated successfully",
            data: await _userService.UpdateUser(id, user)
            )
        );
    }
    #endregion
    
    #region Delete Method
    [HttpDelete(APIEndpointsConstant.AccountEndpoints.DELETE_ACCOUNT_ENDPOINT)]
    public async Task<IActionResult> DeleteAccount([FromRoute] Guid id)
    {
        return Ok(ApiResponseBuilder.BuildResponse(
            statusCode: StatusCodes.Status200OK,
            isSuccess: true,
            message: "Account deleted successfully",
            data: await _userService.DeleteUser(id)
            )
        );
    }
    #endregion
}