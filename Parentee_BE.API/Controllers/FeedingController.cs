using Microsoft.AspNetCore.Mvc;
using Parentee_BE.BLL.Services.Interfaces;
using Parentee_BE.API.Constants;
using Parentee_BE.DAL.Data.Metadatas;
using Parentee_BE.DAL.Data.RequestDTO;
using Parentee_BE.DAL.Data.RequestDTO.Feedings;

namespace Parentee_BE.API.Controllers;

public class FeedingController(ILogger<FeedingController> logger, IFeedingService feedingService) 
    : BaseController<FeedingController>(logger)
{
    [HttpGet(APIEndpointsConstant.FeedingEndpoints.GET_FEEDING_BY_ID_ENDPOINT)]
    public async Task<IActionResult> GetFeedingById([FromRoute] Guid id)
    {
        var feedingById = await feedingService.GetFeedingById(id);
        return Ok(ApiResponseBuilder.BuildResponse(
            statusCode: StatusCodes.Status200OK,
            isSuccess: true,
            message: "Get feeding by id successfully",
            data: feedingById));
    }
    
    [HttpPost(APIEndpointsConstant.FeedingEndpoints.CREATE_FEEDING_ENDPOINT)]
    public async Task<IActionResult> CreateFeeding([FromBody] CreateFeedingRequest request)
    {
        var createResult = await feedingService.CreateFeeding(request);
        return Ok(ApiResponseBuilder.BuildResponse(
            statusCode: StatusCodes.Status201Created,
            isSuccess: true,
            message: "Create feeding successfully",
            data: createResult));
    }

    [HttpPut(APIEndpointsConstant.FeedingEndpoints.UPDATE_FEEDING_ENDPOINT)]
    public async Task<IActionResult> UpdateFeeding([FromRoute] Guid id, [FromBody] UpdateFeedingRequest request)
    {
        var updateResult = await feedingService.UpdateFeeding(id, request);
        return Ok(ApiResponseBuilder.BuildResponse(
            statusCode: StatusCodes.Status200OK,
            isSuccess: true,
            message: "Update feeding successfully",
            data: updateResult));
    }

    [HttpDelete(APIEndpointsConstant.FeedingEndpoints.DELETE_FEEDING_ENDPOINT)]
    public async Task<IActionResult> DeleteFeeding([FromRoute] Guid id)
    {
        var deleteResult = await feedingService.DeleteFeeding(id);
        return Ok(ApiResponseBuilder.BuildResponse(
            statusCode: StatusCodes.Status200OK,
            isSuccess: deleteResult,
            message: deleteResult ? "Delete feeding successfully" : "Failed to delete feeding",
            data: deleteResult));
    }
}
