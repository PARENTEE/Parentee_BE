using Parentee_BE.DAL.Data.Entities;
using Parentee_BE.DAL.Data.ResponseDTO.DiaperChange;
using Parentee_BE.DAL.Data.ResponseDTO.Feedings;
using Parentee_BE.DAL.Data.ResponseDTO.Measurement;
using Parentee_BE.DAL.Data.ResponseDTO.Sleep;

namespace Parentee_BE.DAL.Data.ResponseDTO.Children;

public class GetChildTodayResponse
{
    public Guid Id { get; set; }
    public Guid FamilyId { get; set; }
    public string FullName { get; set; }
    public DateOnly BirthDate { get; set; }
    public string? Sex { get; set; }
    public Guid? PhotoImageId { get; set; }
    public string? Notes { get; set; }
    
    public virtual GetMeasurementResponse Measurement { get; set; }
    public virtual ICollection<GetDiaperChangeResponse> DiaperChanges { get; set; }
    public virtual ICollection<GetFeedingResponse> Feedings { get; set; }
    public virtual ICollection<GetSleepResponse> Sleeps { get; set; }
}