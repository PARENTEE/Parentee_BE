using Microsoft.AspNetCore.Mvc;
using Parentee_BE.BLL.Services.Interfaces;
using Parentee_BE.Constants;
using Parentee_BE.DAL.Data.Metadatas;
using Parentee_BE.DAL.Data.RequestDTO.Ai;

namespace Parentee_BE.Controllers;

public class AiController(ILogger<AiController> logger, IAiService aiService) : BaseController<AiController>(logger)
{
    [HttpPost(APIEndpointsConstant.AiEndpoints.CHAT_ENDPOINT)]
    public async Task<IActionResult> Chat([FromBody] ChatRequestDTO requestDto)
    {
        return Ok(ApiResponseBuilder.BuildResponse(
            statusCode: StatusCodes.Status200OK,
            isSuccess: true,
            message: "Handle chat successful",
            data: await aiService.HandleChat(requestDto)
        ));
    }
}