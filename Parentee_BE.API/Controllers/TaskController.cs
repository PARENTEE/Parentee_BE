using Microsoft.AspNetCore.Mvc;
using Parentee_BE.BLL.Services.Interfaces;
using Parentee_BE.Constants;
using Parentee_BE.DAL.Data.Metadatas;
using Parentee_BE.DAL.Data.RequestDTO.Task;
using TaskStatus = Parentee_BE.DAL.Data.Enums.TaskStatus;

namespace Parentee_BE.API.Controllers;

public class TaskController(ILogger<TaskController> logger, ITaskService taskService)
    : BaseController<TaskController>(logger)
{
    [HttpGet(APIEndpointsConstant.TaskEndpoints.GET_TASK_BY_ID_ENDPOINT)]
    public async Task<IActionResult> GetTaskById([FromRoute] Guid id)
    {
        var result = await taskService.GetTaskById(id);
        return Ok(ApiResponseBuilder.BuildResponse(
            statusCode: StatusCodes.Status200OK,
            isSuccess: true,
            message: "Get task by id successfully",
            data: result));
    }
    
    [HttpGet(APIEndpointsConstant.TaskEndpoints.GET_TASK_BY_ID_AND_DATE_ENDPOINT)]
    public async Task<IActionResult> GetTaskByFamilyIdAndDate([FromRoute] Guid childId, [FromRoute] DateTime date)
    {
        var result = await taskService.GetTasksByFamilyIdAndDate(childId, date);
        return Ok(ApiResponseBuilder.BuildResponse(
            statusCode: StatusCodes.Status200OK,
            isSuccess: true,
            message: "Get task by FamilyId and Date successfully",
            data: result));
    }

    [HttpPost(APIEndpointsConstant.TaskEndpoints.CREATE_TASK_ENDPOINT)]
    public async Task<IActionResult> CreateTask([FromBody] CreateTaskRequest request)
    {
        var result = await taskService.CreateTask(request);
        return Ok(ApiResponseBuilder.BuildResponse(
            statusCode: StatusCodes.Status201Created,
            isSuccess: true,
            message: "Create task successfully",
            data: result));
    }

    [HttpPut(APIEndpointsConstant.TaskEndpoints.UPDATE_TASK_ENDPOINT)]
    public async Task<IActionResult> UpdateTask([FromRoute] Guid id, [FromBody] UpdateTaskRequest request)
    {
        var result = await taskService.UpdateTask(id, request);
        return Ok(ApiResponseBuilder.BuildResponse(
            statusCode: StatusCodes.Status200OK,
            isSuccess: true,
            message: "Update task successfully",
            data: result));
    }
    
    [HttpPut(APIEndpointsConstant.TaskEndpoints.UPDATE_TASK_STATUS_ENDPOINT)]
    public IActionResult UpdateTaskStatus([FromRoute] Guid id, [FromRoute] TaskStatus status)
    {
        var result = taskService.UpdateTaskStatus(id, status);
        return Ok(ApiResponseBuilder.BuildResponse(
            statusCode: StatusCodes.Status200OK,
            isSuccess: true,
            message: "Cập nhật thành công",
            data: result.Result));
    }

    [HttpDelete(APIEndpointsConstant.TaskEndpoints.DELETE_TASK_ENDPOINT)]
    public async Task<IActionResult> DeleteTask([FromRoute] Guid id)
    {
        var result = await taskService.DeleteTask(id);
        return Ok(ApiResponseBuilder.BuildResponse(
            statusCode: StatusCodes.Status200OK,
            isSuccess: result,
            message: result ? "Delete task successfully" : "Failed to delete task",
            data: result));
    }
}