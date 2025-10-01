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
    
    [HttpGet(APIEndpointsConstant.AuthEndpoints.SIGNIN_GOOGLE)]
    public async Task SigninGoogle()
    {
        await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme,
            new AuthenticationProperties
            {
                RedirectUri = Url.Action("GoogleResponse", "Auth")
            });
    }
    
    [HttpGet(APIEndpointsConstant.AuthEndpoints.GOOGLE_RESPONSE)]
    public async Task<IActionResult> GoogleResponse()
    {
        var authenticateResult = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        
        return Ok(ApiResponseBuilder.BuildResponse(
            statusCode: StatusCodes.Status200OK,
            isSuccess: true,
            message: "Signin google successful",
            data: await authService.HandleGoogleLogin(authenticateResult)
        ));
    }
}