using System.ComponentModel.DataAnnotations;

namespace Parentee_BE.DAL.Data.RequestDTO.Family;

public class BaseFamilyRequest
{
    [Required(ErrorMessage = "Family name is required")]
    public string Name { get; set; } = null!;

    // public Guid? CoverImageId { get; set; }
}