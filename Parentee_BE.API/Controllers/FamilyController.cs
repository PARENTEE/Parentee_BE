using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Parentee_BE.BLL.Services.Interfaces;
using Parentee_BE.Constants;
using Parentee_BE.DAL.Data.Entities;
using Parentee_BE.DAL.Data.Metadatas;
using Parentee_BE.DAL.Data.RequestDTO.Family;
using Parentee_BE.DAL.Data.ResponseDTO.Family;

namespace Parentee_BE.API.Controllers;

public class FamilyController(IMapper mapper, ILogger<FamilyController> logger, IFamilyService familyService)
    : BaseController<FamilyController>(logger)
{
    [HttpGet(APIEndpointsConstant.FamilyEndpoints.GET_FAMILY_BY_ID_ENDPOINT)]
    public async Task<IActionResult> GetFamilyById([FromRoute] Guid id)
    {
        var familyById = await familyService.GetFamilyById(id);
        return Ok(ApiResponseBuilder.BuildResponse(
            statusCode: StatusCodes.Status200OK,
            isSuccess: true,
            message: "Get family by id successfully",
            data: familyById));
    }

    [HttpGet(APIEndpointsConstant.FamilyEndpoints.GET_FAMILY_DETAILS_BY_ID_ENDPOINT)]
    public async Task<IActionResult> GetFamilyDetailById([FromRoute] Guid id)
    {
        return Ok(ApiResponseBuilder.BuildResponse(
            statusCode: StatusCodes.Status200OK,
            isSuccess: true,
            message: "Get family detail by id successfully",
            data: await familyService.GetFamilyDetailById(id)
        ));
    }

    [Authorize]
    [HttpPost(APIEndpointsConstant.FamilyEndpoints.CREATE_FAMILY_ENDPOINT)]
    public async Task<IActionResult> CreateFamily([FromBody] string name)
    {
        var createResult = await familyService.CreateFamily(name);
        return Ok(ApiResponseBuilder.BuildResponse(
            statusCode: StatusCodes.Status201Created,
            isSuccess: true,
            message: "Create family successfully",
            data: createResult));
    }
    
    [Authorize]
    [HttpPost(APIEndpointsConstant.FamilyEndpoints.ASSIGN_MEMBER_TO_FAMILY_ENDPOINT)]
    public async Task<IActionResult> AssignMemberToFamily([FromRoute] Guid id, [FromBody] UserFamilyRoleRequest request)
    {
        var userFamilyRole = mapper.Map<UserFamilyRoleEntity>(request);
        var result = await familyService.AddMemberForFamily(id, userFamilyRole);
        var response = mapper.Map<GetFamilyResponse>(result);
        return Ok(ApiResponseBuilder.BuildResponse(
            statusCode: StatusCodes.Status201Created,
            isSuccess: true,
            message: "Mời thành công!",
            data: response));
    }
    
    [Authorize]
    [HttpPost(APIEndpointsConstant.FamilyEndpoints.ACCEPT_INVITATION_ENDPOINT)]
    public async Task<IActionResult> UpdateInvitation([FromRoute] Guid id, [FromRoute] bool isAccepted)
    {
        var result = await familyService.UpdateInvitation(id, isAccepted);
        return Ok(ApiResponseBuilder.BuildResponse(
            statusCode: StatusCodes.Status201Created,
            isSuccess: true,
            message: result ? "Chấp nhận lời mời thành công!" : "Từ chối lời mời thành công!",
            data: result));
    }

    [HttpPut(APIEndpointsConstant.FamilyEndpoints.UPDATE_FAMILY_ENDPOINT)]
    public async Task<IActionResult> UpdateFamily([FromRoute] Guid id, [FromBody] UpdateFamilyRequest request)
    {
        var updateResult = await familyService.UpdateFamily(id, request);
        return Ok(ApiResponseBuilder.BuildResponse(
            statusCode: StatusCodes.Status200OK,
            isSuccess: true,
            message: "Update family successfully",
            data: updateResult));
    }
    
    [Authorize]
    [HttpDelete(APIEndpointsConstant.FamilyEndpoints.DISABLE_FAMILY_ENDPOINT)]
    public async Task<IActionResult> DisableFamily([FromRoute] Guid id)
    {
        var disableResult = await familyService.DisableFamily(id);
        return Ok(ApiResponseBuilder.BuildResponse(
            statusCode: StatusCodes.Status200OK,
            isSuccess: disableResult,
            message: disableResult ? "Disable family successfully" : "Failed to disable family",
            data: disableResult));
    }

    [HttpDelete(APIEndpointsConstant.FamilyEndpoints.DELETE_FAMILY_ENDPOINT)]
    public async Task<IActionResult> DeleteFamily([FromRoute] Guid id)
    {
        var deleteResult = await familyService.DeleteFamily(id);
        return Ok(ApiResponseBuilder.BuildResponse(
            statusCode: StatusCodes.Status200OK,
            isSuccess: deleteResult,
            message: deleteResult ? "Delete family successfully" : "Failed to delete family",
            data: deleteResult));
    }
}