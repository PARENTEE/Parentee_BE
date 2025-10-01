using Microsoft.AspNetCore.Mvc;
using Parentee_BE.BLL.Services.Interfaces;
using Parentee_BE.API.Constants;
using Parentee_BE.DAL.Data.Metadatas;
using Parentee_BE.DAL.Data.RequestDTO.DiaperChange;

namespace Parentee_BE.API.Controllers;

public class DiaperChangeController(ILogger<DiaperChangeController> logger, IDiaperChangeService diaperChangeService) 
        : BaseController<DiaperChangeController>(logger)
    {
        [HttpGet(APIEndpointsConstant.DiaperChangeEndpoints.GET_DIAPERCHANGE_BY_ID_ENDPOINT)]
        public async Task<IActionResult> GetDiaperChangeById([FromRoute] Guid id)
        {
            var result = await diaperChangeService.GetDiaperChangeById(id);
            return Ok(ApiResponseBuilder.BuildResponse(
                statusCode: StatusCodes.Status200OK,
                isSuccess: true,
                message: "Get diaper change by id successfully",
                data: result));
        }

        [HttpPost(APIEndpointsConstant.DiaperChangeEndpoints.CREATE_DIAPERCHANGE_ENDPOINT)]
        public async Task<IActionResult> CreateDiaperChange([FromBody] CreateDiaperChangeRequest request)
        {
            var result = await diaperChangeService.CreateDiaperChange(request);
            return Ok(ApiResponseBuilder.BuildResponse(
                statusCode: StatusCodes.Status201Created,
                isSuccess: true,
                message: "Create diaper change successfully",
                data: result));
        }

        [HttpPut(APIEndpointsConstant.DiaperChangeEndpoints.UPDATE_DIAPERCHANGE_ENDPOINT)]
        public async Task<IActionResult> UpdateDiaperChange([FromRoute] Guid id, [FromBody] UpdateDiaperChangeRequest request)
        {
            var result = await diaperChangeService.UpdateDiaperChange(id, request);
            return Ok(ApiResponseBuilder.BuildResponse(
                statusCode: StatusCodes.Status200OK,
                isSuccess: true,
                message: "Update diaper change successfully",
                data: result));
        }

        [HttpDelete(APIEndpointsConstant.DiaperChangeEndpoints.DELETE_DIAPERCHANGE_ENDPOINT)]
        public async Task<IActionResult> DeleteDiaperChange([FromRoute] Guid id)
        {
            var result = await diaperChangeService.DeleteDiaperChange(id);
            return Ok(ApiResponseBuilder.BuildResponse(
                statusCode: StatusCodes.Status200OK,
                isSuccess: result,
                message: result ? "Delete diaper change successfully" : "Failed to delete diaper change",
                data: result));
        }
    }