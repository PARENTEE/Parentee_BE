using Google.Apis.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Parentee_BE.BLL.Services.Interfaces;
using Parentee_BE.DAL.Data.Metadatas;
using Parentee_BE.DAL.Data.RequestDto.Auth;
using Microsoft.AspNetCore.Mvc;
using Parentee_BE.Constants;

namespace Parentee_BE.API.Controllers;

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

    [HttpPost(APIEndpointsConstant.AuthEndpoints.SIGNIN_GOOGLE)]
    public async Task<IActionResult> GoogleSignIn([FromBody] GoogleSignInRequest request)
    {
        var token = await authService.HandleGoogleLogin(request.Email, request.FullName);

        return Ok(ApiResponseBuilder.BuildResponse(
            200,
            true,
            "Google sign-in successful",
            token
        ));
    }
}