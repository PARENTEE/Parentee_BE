using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Parentee_BE.AI.Arugments;
using Parentee_BE.AI.Services;
using Parentee_BE.BLL.Services.Interfaces;
using Parentee_BE.Constants;
using Parentee_BE.DAL.Data.Metadatas;
using Parentee_BE.DAL.Data.RequestDTO.Ai;

namespace Parentee_BE.API.Controllers;

public class AiController(ILogger<AiController> logger, RagChatService ragChatService,IHttpContextAccessor httpContextAccessor) : BaseController<AiController>(logger)
{
    [Authorize]
    [HttpPost(APIEndpointsConstant.AiEndpoints.CHAT_ENDPOINT)]
    public async Task<IActionResult> Chat([FromBody] ChatRequestDTO requestDto)
    {
        return Ok(ApiResponseBuilder.BuildResponse(
            statusCode: StatusCodes.Status200OK,
            isSuccess: true,
            message: "Handle chat successful",
            data: await ragChatService.ChatAnswer(requestDto.Message)
        ));
    }
}