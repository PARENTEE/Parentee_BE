using Microsoft.AspNetCore.Mvc;
using Parentee_BE.BLL.Services.Interfaces;
using Parentee_BE.Constants;
using Parentee_BE.DAL.Data.Metadatas;
using Parentee_BE.DAL.Data.RequestDTO.SolidFood;

namespace Parentee_BE.API.Controllers;

public class SolidFoodController(ILogger<SolidFoodController> logger, ISolidFoodService solidFoodService)
    : BaseController<SolidFoodController>(logger)
{
    [HttpPost(APIEndpointsConstant.SolidFoodEndpoints.CREATE_ENDPOINT)]
    public async Task<IActionResult> CreateSleep([FromBody] CreateSolidFoodRequest request)
    {
        var result = await solidFoodService.CreateSolidFood(request);
        return Ok(ApiResponseBuilder.BuildResponse(
            statusCode: StatusCodes.Status201Created,
            isSuccess: true,
            message: "Create solid food record successfully",
            data: result));
    }
}
