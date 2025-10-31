using Parentee_BE.DAL.Data.RequestDTO.Task;
using Parentee_BE.DAL.Data.ResponseDTO.Task;
using TaskStatus = Parentee_BE.DAL.Data.Enums.TaskStatus;

namespace Parentee_BE.BLL.Services.Interfaces;

public interface ITaskService
{
    Task<GetTaskResponse> GetTaskById(Guid id);
    Task<ICollection<GetTaskResponse>> GetTasksByFamilyIdAndDate(Guid familyId, DateTime date);
    Task<GetTaskResponse> CreateTask(CreateTaskRequest requestDto);
    Task<GetTaskResponse> UpdateTask(Guid id, UpdateTaskRequest requestDto);
    Task<int> UpdateTaskStatus(Guid id, TaskStatus status);
    Task<bool> DeleteTask(Guid id);
}