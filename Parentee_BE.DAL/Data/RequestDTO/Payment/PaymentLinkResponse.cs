namespace Parentee_BE.DAL.Data.RequestDTO.Payment;

public class PaymentDataResponse
{
    public long OrderCode { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; }
    public string Status { get; set; }
    public string CheckoutUrl { get; set; }
    public DateTime ExpiredAt { get; set; }
}