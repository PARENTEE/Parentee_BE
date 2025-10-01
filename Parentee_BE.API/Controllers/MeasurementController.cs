using Microsoft.AspNetCore.Mvc;
using Parentee_BE.BLL.Services.Interfaces;
using Parentee_BE.API.Constants;
using Parentee_BE.DAL.Data.Metadatas;
using Parentee_BE.DAL.Data.RequestDTO.Measurement;

namespace Parentee_BE.API.Controllers;

public class MeasurementController(ILogger<MeasurementController> logger, IMeasurementService measurementService)
    : BaseController<MeasurementController>(logger)
{
    [HttpGet(APIEndpointsConstant.MeasurementEndpoints.GET_MEASUREMENT_BY_ID_ENDPOINT)]
    public async Task<IActionResult> GetMeasurementById([FromRoute] Guid id)
    {
        var result = await measurementService.GetMeasurementById(id);
        return Ok(ApiResponseBuilder.BuildResponse(
            statusCode: StatusCodes.Status200OK,
            isSuccess: true,
            message: "Get measurement by id successfully",
            data: result));
    }

    [HttpPost(APIEndpointsConstant.MeasurementEndpoints.CREATE_MEASUREMENT_ENDPOINT)]
    public async Task<IActionResult> CreateMeasurement([FromBody] CreateMeasurementRequest request)
    {
        var result = await measurementService.CreateMeasurement(request);
        return Ok(ApiResponseBuilder.BuildResponse(
            statusCode: StatusCodes.Status201Created,
            isSuccess: true,
            message: "Create measurement successfully",
            data: result));
    }

    [HttpPut(APIEndpointsConstant.MeasurementEndpoints.UPDATE_MEASUREMENT_ENDPOINT)]
    public async Task<IActionResult> UpdateMeasurement([FromRoute] Guid id, [FromBody] UpdateMeasurementRequest request)
    {
        var result = await measurementService.UpdateMeasurement(id, request);
        return Ok(ApiResponseBuilder.BuildResponse(
            statusCode: StatusCodes.Status200OK,
            isSuccess: true,
            message: "Update measurement successfully",
            data: result));
    }

    [HttpDelete(APIEndpointsConstant.MeasurementEndpoints.DELETE_MEASUREMENT_ENDPOINT)]
    public async Task<IActionResult> DeleteMeasurement([FromRoute] Guid id)
    {
        var result = await measurementService.DeleteMeasurement(id);
        return Ok(ApiResponseBuilder.BuildResponse(
            statusCode: StatusCodes.Status200OK,
            isSuccess: result,
            message: result ? "Delete measurement successfully" : "Failed to delete measurement",
            data: result));
    }
}