using System.ComponentModel.DataAnnotations;
using Parentee_BE.DAL.Data.Enums;

namespace Parentee_BE.DAL.Data.RequestDTO.DiaperChange;

public class BaseDiaperChangeRequest
{
    [Required(ErrorMessage = "Child Id is required.")]
    public Guid ChildId { get; set; }

    [Required(ErrorMessage = "ChangedAt is required.")]
    public DateTime ChangedAt { get; set; }

    [Required(ErrorMessage = "Diaper type is required.")]
    public DiaperType Type { get; set; }
    
    [Required(ErrorMessage = "Diaper quantity is required.")]
    public DiaperQuantity DiaperQuantity { get; set; }
    
    [Required(ErrorMessage = "Color is required.")]
    public string? Color { get; set; }
    
    [Required(ErrorMessage = "Diaper waste is required.")]
    public DiaperWaste DiaperWaste { get; set; }

    [MaxLength(500, ErrorMessage = "Notes cannot exceed 500 characters.")]
    public string? Notes { get; set; }
}