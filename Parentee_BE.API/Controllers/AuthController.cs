using Parentee_BE.BLL.Services.Interfaces;
using Parentee_BE.Constants;
using Parentee_BE.DAL.Data.Metadatas;
using Parentee_BE.DAL.Data.RequestDto.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Parentee_BE.Controllers;

public class AuthController(
    ILogger<AuthController> logger,
    IAuthService authService
) : BaseController<AuthController>(logger)
{
    [HttpPost(APIEndpointsConstant.AuthEndpoints.LOGIN_ENDPOINT)]
    public async Task<IActionResult> Login([FromBody] LoginRequestDTO requestDto)
    {
        return Ok(ApiResponseBuilder.BuildResponse(
            statusCode: StatusCodes.Status200OK,
            isSuccess: true,
            message: "Login successful",
            data: await authService.HandleLogin(requestDto)
        ));
    }
   
}