using TaskStatus = Parentee_BE.DAL.Data.Enums.TaskStatus;

namespace Parentee_BE.DAL.Data.ResponseDTO.Task;

public class GetTaskResponse
{
    public Guid Id { get; set; }
    public Guid FamilyId { get; set; }
    public Guid? ChildId { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime? StartsAt { get; set; }
    public DateTime? EndsAt { get; set; }
    public bool AllDay { get; set; }
    public TaskStatus Status { get; set; }
    public Guid? CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}