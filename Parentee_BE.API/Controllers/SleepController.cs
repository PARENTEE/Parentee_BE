using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Parentee_BE.API.Constants;
using Parentee_BE.BLL.Services.Interfaces;
using Parentee_BE.DAL.Data.Metadatas;
using Parentee_BE.DAL.Data.RequestDTO.Sleep;

namespace Parentee_BE.API.Controllers
{
    public class SleepController(ILogger<SleepController> logger, ISleepService sleepService) 
        : BaseController<SleepController>(logger)
    {
        [HttpGet(APIEndpointsConstant.SleepEndpoints.GET_SLEEP_BY_ID_ENDPOINT)]
        public async Task<IActionResult> GetSleepById([FromRoute] Guid id)
        {
            var result = await sleepService.GetSleepById(id);
            return Ok(ApiResponseBuilder.BuildResponse(
                statusCode: StatusCodes.Status200OK,
                isSuccess: true,
                message: "Get sleep by id successfully",
                data: result));
        }

        [HttpPost(APIEndpointsConstant.SleepEndpoints.CREATE_SLEEP_ENDPOINT)]
        public async Task<IActionResult> CreateSleep([FromBody] CreateSleepRequest request)
        {
            var result = await sleepService.CreateSleep(request);
            return Ok(ApiResponseBuilder.BuildResponse(
                statusCode: StatusCodes.Status201Created,
                isSuccess: true,
                message: "Create sleep record successfully",
                data: result));
        }

        [HttpPut(APIEndpointsConstant.SleepEndpoints.UPDATE_SLEEP_ENDPOINT)]
        public async Task<IActionResult> UpdateSleep([FromRoute] Guid id, [FromBody] UpdateSleepRequest request)
        {
            var result = await sleepService.UpdateSleep(id, request);
            return Ok(ApiResponseBuilder.BuildResponse(
                statusCode: StatusCodes.Status200OK,
                isSuccess: true,
                message: "Update sleep record successfully",
                data: result));
        }

        [HttpDelete(APIEndpointsConstant.SleepEndpoints.DELETE_SLEEP_ENDPOINT)]
        public async Task<IActionResult> DeleteSleep([FromRoute] Guid id)
        {
            var result = await sleepService.DeleteSleep(id);
            return Ok(ApiResponseBuilder.BuildResponse(
                statusCode: StatusCodes.Status200OK,
                isSuccess: result,
                message: result ? "Delete sleep record successfully" : "Failed to delete sleep record",
                data: result));
        }
    }
}
