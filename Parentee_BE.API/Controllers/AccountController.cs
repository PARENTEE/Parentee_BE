using Parentee_BE.ActionFilters;
using Parentee_BE.BLL.Services.Interfaces;
using Parentee_BE.Constants;
using Parentee_BE.DAL.Data.Entities;
using Parentee_BE.DAL.Data.Exceptions;
using Parentee_BE.DAL.Data.Metadatas;
using Parentee_BE.DAL.Data.RequestDTO.Accounts;
using Parentee_BE.DAL.Data.ResponseDTO.Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Parentee_BE.Controllers;

public class AccountController : BaseController<AccountController>
{
    #region Create Class Reference
    private readonly IAccountService _accountService;
    #endregion
    
    #region Constructors
    public AccountController (ILogger<AccountController> logger, IAccountService accountService) : base(logger)
    {
        _accountService = accountService;
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
                data:  await _accountService.GetCurrentAccount()
            )
        );
    }
    
    [Authorize(Roles = $"{RoleEntity.Admin}, {RoleEntity.User}")]
    [HttpGet(APIEndpointsConstant.AccountEndpoints.GET_MANY_ACCOUNTS_ENDPOINT)]
    public async Task<IActionResult> GetMany(
        [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10
        )
    {
        return Ok(ApiResponseBuilder.BuildResponse(
                statusCode: StatusCodes.Status201Created,
                isSuccess: true,
                message: "Get many accounts successfully",
                data:  await _accountService.GetManyAccounts(pageSize: pageSize, pageNumber: pageNumber)
            )
        );
    }
    
    [Authorize(Roles = $"{RoleEntity.Admin}, {RoleEntity.User}")]
    [HttpGet(APIEndpointsConstant.AccountEndpoints.GET_ACCOUNT_BY_ID_ENDPOINT)]
    public async Task<IActionResult> GetAccountById([FromRoute] Guid id)
    {
        return Ok(ApiResponseBuilder.BuildResponse(
                statusCode: StatusCodes.Status201Created,
                isSuccess: true,
                message: "Account created successfully",
                data:  await _accountService.GetAccountById(id)
                )
        );
    }
    #endregion
    
    #region Post Method
    
    [HttpPost(APIEndpointsConstant.AccountEndpoints.CREATE_ACCOUNT_ENDPOINT)]
    [ValidAttributeActionFilter]
    public async Task<IActionResult> CreateAccount([FromBody] CreateAccountRequestDTO requestDto)
    {
        return Ok(ApiResponseBuilder.BuildResponse(
            statusCode: StatusCodes.Status201Created,
            isSuccess: true,
            message: "Account created successfully",
            data: await _accountService.CreateAccount(requestDto)
            )
        );
    }
    
    #endregion
    
    #region Put Method
    [HttpPut(APIEndpointsConstant.AccountEndpoints.UPDATE_ACCOUNT_ENDPOINT)]
    [ValidAttributeActionFilter]
    public async Task<IActionResult> UpdateAccount(
        [FromRoute] Guid id,
        [FromBody] UpdateAccountRequestDTO account)
    {
        return Ok(ApiResponseBuilder.BuildResponse(
            statusCode: StatusCodes.Status200OK,
            isSuccess: true,
            message: "Account updated successfully",
            data: await _accountService.UpdateAccount(id, account)
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
            data: await _accountService.DeleteAccount(id)
            )
        );
    }
    #endregion
}