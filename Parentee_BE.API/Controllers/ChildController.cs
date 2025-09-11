using Microsoft.AspNetCore.Mvc;
using Parentee_BE.BLL.Services.Interfaces;
using Parentee_BE.Constants;
using Parentee_BE.DAL.Data.Entities;
using Parentee_BE.DAL.Data.Metadatas;
using Parentee_BE.DAL.Data.RequestDTO.Children;

namespace Parentee_BE.Controllers;

public class ChildController(IChildService childService, ILogger<ChildController> logger) : BaseController<ChildController>(logger)
{
    [HttpPost(APIEndpointsConstant.ChildEndpoints.CREATE_CHILD_ENDPOINT)]
    public async Task<IActionResult> CreateChild([FromBody] CreateChildRequestDTO request)
    {
        var createResult = await childService.CreateChild(request);
        return Ok(ApiResponseBuilder.BuildResponse(
            statusCode: StatusCodes.Status201Created,
            isSuccess: true,
            message: "Create child successfully",
            data: createResult));
    }

    [HttpGet(APIEndpointsConstant.ChildEndpoints.VIEW_CHILD_ENDPOINT)]
    public async Task<IActionResult> GetAllChildren()
    {
        var children = await childService.GetAllChildren();
        return Ok(ApiResponseBuilder.BuildResponse(
            statusCode: StatusCodes.Status201Created,
            isSuccess: true,
            message: "Get all children successfully",
            data: children));
    }

    [HttpGet(APIEndpointsConstant.ChildEndpoints.GET_CHILD_BY_ID_ENDPOINT)]
    public async Task<IActionResult> GetChildById(Guid id)
    {
        var child = await childService.GetChildById(id);
        if (child == null)
        {
            return NotFound(ApiResponseBuilder.BuildResponse<ChildEntity>(
                statusCode: StatusCodes.Status404NotFound,
                isSuccess: false,
                message: "Child not found",
                data: null
            ));
        }

        return Ok(ApiResponseBuilder.BuildResponse(
            statusCode: StatusCodes.Status200OK,
            isSuccess: true,
            message: "Get child successfully",
            data: child));
    }

    [HttpDelete(APIEndpointsConstant.ChildEndpoints.DELETE_CHILD_ENDPOINT)]
    public async Task<IActionResult> DeleteChild(Guid id)
    {
        var delete = await childService.DeleteChild(id);
        if (!delete)
        {
            return NotFound(ApiResponseBuilder.BuildResponse<bool>(
                statusCode: StatusCodes.Status404NotFound,
                isSuccess: false,
                message: "Child not found",
                data: false
            ));
        }
        return Ok(ApiResponseBuilder.BuildResponse(
            statusCode: StatusCodes.Status200OK,
            isSuccess: true,
            message: "Delete child successfully",
            data: true));
    }

    [HttpPut(APIEndpointsConstant.ChildEndpoints.UPDATE_CHILD_ENDPOINT)]
    public async Task<IActionResult> UpdateChild(Guid id, [FromBody] CreateChildRequestDTO request)
    {
        var updatedChild = await childService.UpdateChild(id, request);
        if (updatedChild == null)
        {
            return NotFound(ApiResponseBuilder.BuildResponse<bool>(
                statusCode: StatusCodes.Status404NotFound,
                isSuccess: false,
                message: "Child not found",
                data: false
            ));
        }
        return Ok(ApiResponseBuilder.BuildResponse(
            statusCode: StatusCodes.Status200OK,
            isSuccess: true,
            message: "Update child successfully",
            data: true));
    }
}