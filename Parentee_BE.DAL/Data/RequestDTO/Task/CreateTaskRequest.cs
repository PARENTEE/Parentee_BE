using System.ComponentModel.DataAnnotations;

namespace Parentee_BE.DAL.Data.RequestDTO.Task;

public class CreateTaskRequest : BaseTaskRequest
{
    [Required(ErrorMessage = "FamilyId is required.")]
    public Guid FamilyId { get; set; }

    public Guid? ChildId { get; set; }

}