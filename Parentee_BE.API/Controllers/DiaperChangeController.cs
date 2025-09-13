using Microsoft.AspNetCore.Mvc;
using Parentee_BE.Constants;
using Parentee_BE.DAL.Data.Entities;
using Parentee_BE.DAL.Data.Metadatas;
using Parentee_BE.DAL.Data.RequestDTO.DiaperChange;

namespace Parentee_BE.Controllers;

public class DiaperChangeController(IDiaperChangeService diaperChangeService, ILogger<DiaperChangeController> logger) : BaseController<DiaperChangeController>(logger)
{
    [HttpPost(APIEndpointsConstant.DiaperEnpoints.CREATE_DIAPER_CHANGE_ENDPOINT)]
    public async Task<IActionResult> CreateDiaperChange([FromBody] CreateDiaperChangeRequest request)
    {
        var response = await diaperChangeService.CreateDiaperChange(request);
        return Ok(ApiResponseBuilder.BuildResponse(
            statusCode: StatusCodes.Status201Created,
            isSuccess: true,
            message: "Create diaper changes successfully",
            data: response));
    }

    [HttpPut(APIEndpointsConstant.DiaperEnpoints.UPDATE_DIAPER_CHANGE_ENDPOINT)]
    public async Task<IActionResult> UpdateDiaperChange(Guid childId, [FromBody] UpdateDiaperChangeRequest request)
    {
        var response = await diaperChangeService.UpdateDiaperChange(childId, request);
        return Ok(ApiResponseBuilder.BuildResponse(
            statusCode: StatusCodes.Status201Created,
            isSuccess: true,
            message: "Update diaper changes successfully",
            data: response));
    }
}