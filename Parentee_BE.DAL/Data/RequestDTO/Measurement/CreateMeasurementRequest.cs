using System.ComponentModel.DataAnnotations;

namespace Parentee_BE.DAL.Data.RequestDTO.Measurement;

public class CreateMeasurementRequest : BaseMeasurementRequest
{
    [Required(ErrorMessage = "FamilyId is required.")]
    public Guid FamilyId { get; set; }

    [Required(ErrorMessage = "ChildId is required.")]
    public Guid ChildId { get; set; }
}