using TaskStatus = Parentee_BE.DAL.Data.Enums.TaskStatus;

namespace Parentee_BE.DAL.Data.RequestDTO.Task;

public class UpdateTaskRequest
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime? StartsAt { get; set; }
    public DateTime? EndsAt { get; set; }
    public bool? AllDay { get; set; }
    public TaskStatus? Status { get; set; }
}