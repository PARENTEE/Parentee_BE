namespace Parentee_BE.DAL.Data.ResponseDTO.Sleep;

public class GetSleepResponse
{
    public Guid Id { get; set; }
    public Guid ChildId { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime? EndedAt { get; set; }
    public string? Location { get; set; }
    public string? Notes { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}