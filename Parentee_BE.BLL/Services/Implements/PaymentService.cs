using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Net.PayOSHQ;
using Net.PayOSHQ.Types;
using Parentee_BE.BLL.Services.Interfaces;
using Parentee_BE.DAL.Context;
using Parentee_BE.DAL.Data.Entities;
using Parentee_BE.DAL.Data.PaymentDTO;
using Parentee_BE.DAL.Data.Repositories.Interfaces;

namespace Parentee_BE.BLL.Services.Implements;

public class PaymentService(
    IUnitOfWork<AppDbContext> unitOfWork, 
    ILogger<PurchaseEntity> _logger, 
    IProductService _productService,
    PayOS _payOs, 
    IOptions<PayOSOptions> _payOpts,
    IPurchaseService _purchaseService
) : BaseService<PurchaseEntity>(unitOfWork, _logger), IPaymentService
{
    public async Task<PaymentData> GetPaymentData(Guid productId, Guid priceId)
    {
        var product = await _productService.GetProductAndPriceAsync(productId, priceId);
        
        var orderCode = GenerateOrderCode();
        var items = new List<ItemData>
        {
            new ItemData(
                name: $"{product.Name} - {product.PriceType}", // label what user bought
                quantity: 1,
                price: (int) product.Amount
            )
        };
        
        var payment = new PaymentData(
            orderCode:   (int)orderCode,
            amount:      (int) product.Amount,
            description: $"Purchase {product.Name}",
            items:       items,
            cancelUrl:   _payOpts.Value.CancelUrl,
            returnUrl:   _payOpts.Value.ReturnUrl
        );

        return payment;
    }
    
    private static long GenerateOrderCode()
    {
        var rnd = new Random();
        return rnd.NextInt64(1_000_000_000, 9_999_999_999); // 10-digit positive
    }
}