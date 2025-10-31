using System.ComponentModel.DataAnnotations;
using TaskStatus = Parentee_BE.DAL.Data.Enums.TaskStatus;

namespace Parentee_BE.DAL.Data.RequestDTO.Task;

public class BaseTaskRequest
{
    [Required(ErrorMessage = "Title is required.")]
    [MaxLength(200, ErrorMessage = "Title cannot exceed 200 characters.")]
    public string Title { get; set; } = null!;

    public DateTime StartsAt { get; set; }
    public DateTime EndsAt { get; set; }
    public bool AllDay { get; set; } = false;

    [Required(ErrorMessage = "Status is required.")]
    public TaskStatus Status { get; set; }
}