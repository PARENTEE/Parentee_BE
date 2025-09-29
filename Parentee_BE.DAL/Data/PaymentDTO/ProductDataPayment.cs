using Parentee_BE.DAL.Data.Enums;

namespace Parentee_BE.DAL.Data.PaymentDTO;

public class ProductDataPayment
{
    public Guid      Id        { get; set; }          
    public string    Name      { get; set; } = default!;
    public Guid      PriceId   { get; set; }          
    public PriceType PriceType { get; set; }         
    public decimal   Amount    { get; set; }         
    public string?    Currency  { get; set; } = "VND"; 
}