using System.ComponentModel.DataAnnotations;

namespace Parentee_BE.DAL.Data.RequestDTO.Task;

public class BaseTaskRequest
{
    [Required(ErrorMessage = "Title is required.")]
    [MaxLength(200, ErrorMessage = "Title cannot exceed 200 characters.")]
    public string Title { get; set; } = null!;

    [MaxLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
    public string? Description { get; set; }

    public DateTime? StartsAt { get; set; }
    public DateTime? EndsAt { get; set; }
    public bool AllDay { get; set; } = false;

    [Required(ErrorMessage = "Status is required.")]
    public TaskStatus Status { get; set; }
}