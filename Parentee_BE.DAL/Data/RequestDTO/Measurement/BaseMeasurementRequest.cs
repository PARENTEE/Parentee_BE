using System.ComponentModel.DataAnnotations;
using Parentee_BE.DAL.Data.Enums;

namespace Parentee_BE.DAL.Data.RequestDTO.Measurement;

public class BaseMeasurementRequest
{
    [Required(ErrorMessage = "Measure type is required.")]
    public MeasureType Type { get; set; }

    [Required(ErrorMessage = "MeasuredAt is required.")]
    public DateTime MeasuredAt { get; set; }

    [Required(ErrorMessage = "Value is required.")]
    [Range(0.01, 999999.99, ErrorMessage = "Value must be greater than 0.")]
    public decimal Value { get; set; }

    [Required(ErrorMessage = "Unit is required.")]
    [MaxLength(20, ErrorMessage = "Unit cannot exceed 20 characters.")]
    public string Unit { get; set; } = null!;

    [MaxLength(100, ErrorMessage = "Source cannot exceed 100 characters.")]
    public string? Source { get; set; }

    [MaxLength(500, ErrorMessage = "Notes cannot exceed 500 characters.")]
    public string? Notes { get; set; }
}