using Net.payOS.Types;

namespace Parentee_BE.BLL.Services.Interfaces;

public interface IPaymentService
{
    Task<PaymentData> GetPaymentData(Guid productId, Guid priceId);
}