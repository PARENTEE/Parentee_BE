using System.ComponentModel.DataAnnotations;
using Parentee_BE.DAL.Data.Enums;

namespace Parentee_BE.DAL.Data.RequestDTO.DiaperChange;

public class BaseDiaperChangeRequest
{
    [Required(ErrorMessage = "ChangedAt is required.")]
    public DateTime ChangedAt { get; set; }

    [Required(ErrorMessage = "Diaper type is required.")]
    public DiaperType Type { get; set; }

    public bool? RashObserved { get; set; }

    [MaxLength(500, ErrorMessage = "Notes cannot exceed 500 characters.")]
    public string? Notes { get; set; }
}