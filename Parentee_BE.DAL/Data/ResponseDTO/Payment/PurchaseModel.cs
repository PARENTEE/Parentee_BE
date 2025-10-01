using Parentee_BE.DAL.Data.Enums;

namespace Parentee_BE.DAL.Data.ResponseDTO.Payment;

public class PurchaseModel
{
    public long OrderCode { get; set; }
    
    public Guid UserId { get; set; }

    public Guid? FamilyId { get; set; }

    public Guid ProductId { get; set; }

    public Guid? PriceId { get; set; }
    
    public string? ProviderTxnId { get; set; }
    
    public PurchaseStatus Status { get; set; }

    public decimal Amount { get; set; }

    public string Currency { get; set; } = null!;

    public string? RawPayload { get; set; }

    public DateTime? PaidAt { get; set; } = null;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; } = null;


}