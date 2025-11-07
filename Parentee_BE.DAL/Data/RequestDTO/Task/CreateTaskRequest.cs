using System.ComponentModel.DataAnnotations;

namespace Parentee_BE.DAL.Data.RequestDTO.Task;

public class CreateTaskRequest : BaseTaskRequest
{
    public Guid? ChildId { get; set; }
    
    public Guid? AssignedTo { get; set; }
}