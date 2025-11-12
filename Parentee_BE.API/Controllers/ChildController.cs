using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Parentee_BE.AI.Plugins.PluginDto;
using Parentee_BE.BLL.Services.Interfaces;
using Parentee_BE.Constants;
using Parentee_BE.DAL.Data.Entities;
using Parentee_BE.DAL.Data.Metadatas;
using Parentee_BE.DAL.Data.RequestDTO.Children;
using Parentee_BE.DAL.Data.ResponseDTO.Children;

namespace Parentee_BE.API.Controllers;

public class ChildController(IChildService childService, IMapper mapper, ILogger<ChildController> logger) : BaseController<ChildController>(logger)
{
    [HttpPost(APIEndpointsConstant.ChildEndpoints.CREATE_CHILD_ENDPOINT)]
    public async Task<IActionResult> CreateChild([FromBody] CreateChildRequestDTO request)
    {
        var createResult = await childService.CreateChild(request);
        return Ok(ApiResponseBuilder.BuildResponse(
            statusCode: StatusCodes.Status201Created,
            isSuccess: true,
            message: "Tạo em bé thành công",
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

    [HttpGet(APIEndpointsConstant.ChildEndpoints.GET_CHILDREN_IN_CURRENT_FAMILY_ENDPOINT)]
    public async Task<IActionResult> GetChildrenInCurrentFamily()
    {
        var children = await childService.GetChildrenInCurrentFamily();
        return Ok(ApiResponseBuilder.BuildResponse(
            statusCode: StatusCodes.Status201Created,
            isSuccess: true,
            message: "Get children in current family successfully",
            data: children));
    }
    
    [HttpGet(APIEndpointsConstant.ChildEndpoints.GET_CHILD_REPORT_ENDPOINT)]
    public async Task<IActionResult> GetChildReport([FromRoute] Guid id,[FromRoute] DateTime date)
    {
        var child = await childService.GetChildReport(id, date);
        return Ok(ApiResponseBuilder.BuildResponse(
            statusCode: StatusCodes.Status201Created,
            isSuccess: true,
            message: "Get children in current family successfully",
            data: mapper.Map<GetChildReportResponse>(child)));
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
    
    [HttpGet(APIEndpointsConstant.ChildEndpoints.GET_CHILD_STATUS_BY_ID_ENDPOINT)]
    public async Task<IActionResult> GetChildStatus(
        [FromQuery] DateTime date,
        [FromQuery] string childName)
    {
        var child = await childService.GetChildStatus(null, date, childName);
        
        return Ok(ApiResponseBuilder.BuildResponse(
            statusCode: StatusCodes.Status200OK,
            isSuccess: true,
            message: "Lấy dữ liệu bé thành công",
            data: mapper.Map<GetChildTodayForAiResponse>(child)
        ));
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
        return Ok(ApiResponseBuilder.BuildResponse(
            statusCode: StatusCodes.Status200OK,
            isSuccess: true,
            message: "Cập nhật thông tin em bé thành công",
            data: updatedChild));
    }
}