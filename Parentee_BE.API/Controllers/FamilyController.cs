using Microsoft.AspNetCore.Mvc;
using Parentee_BE.BLL.Services.Interfaces;
using Parentee_BE.API.Constants;
using Parentee_BE.DAL.Data.Metadatas;
using Parentee_BE.DAL.Data.RequestDTO.Family;

namespace Parentee_BE.API.Controllers;

public class FamilyController(ILogger<FamilyController> logger, IFamilyService familyService) 
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

    [HttpPost(APIEndpointsConstant.FamilyEndpoints.CREATE_FAMILY_ENDPOINT)]
    public async Task<IActionResult> CreateFamily([FromBody] CreateFamilyRequest request)
    {
        var createResult = await familyService.CreateFamily(request);
        return Ok(ApiResponseBuilder.BuildResponse(
            statusCode: StatusCodes.Status201Created,
            isSuccess: true,
            message: "Create family successfully",
            data: createResult));
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
