using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Net.payOS;
using Net.payOS.Types;
using Parentee_BE.API.Constants;
using Parentee_BE.BLL.Services.Interfaces;
using Parentee_BE.DAL.Data.Entities;
using Parentee_BE.DAL.Data.Enums;
using Parentee_BE.DAL.Data.PaymentDTO;

namespace Parentee_BE.API.Controllers;

public class PaymentController(ILogger<PaymentController> _logger, PayOS _payOs, IOptions<PayOSOptions> _payOpts, IProductService _productService) : BaseController<PaymentController>(_logger)
{
    [HttpGet(APIEndpointsConstant.PaymentEndpoints.CREATE_LINK)]
    public async Task<IActionResult> CreatePaymnetLink(Guid productId, Guid priceId, Guid userId)
    {
        var product = await _productService.GetProductAndPriceAsync(productId, priceId);

        var orderCode = GenerateOrderCode();
        var items = new List<ItemData>
        {
            new ItemData(
                name: $"{product.Name} - {product.PriceType == PriceType.RecurringMonth}", // label what user bought
                quantity: 1,
                price: (int) product.Amount
            )
        };
        
        var payment = new PaymentData(
            orderCode:   orderCode,
            amount:      (int) product.Amount,
            description: $"Purchase {product.Name}",
            items:       items,
            cancelUrl:   _payOpts.Value.CancelUrl,
            returnUrl:   _payOpts.Value.ReturnUrl
        );
        var res = await _payOs.createPaymentLink(payment);

        // TODO: persist a Purchase row:
        // product_id = product.Id, price_id = price.Id, amount = price.Amount, status = Pending,
        // provider_txn_id/res.paymentLinkId, order_code, created_at, etc.

        return Ok(new
        {
            orderCode = res.orderCode,
            priceId = product.PriceId,
            priceType = product.PriceType == PriceType.RecurringMonth,
            amount = product.Amount,
            currency = product.Currency,
            checkoutUrl = res.checkoutUrl,
            // qrCode = NormalizeQr(res.qrCode), //Gen QR Image
            status = res.status,
            expiredAt = res.expiredAt
        });
    }
    
    private static long GenerateOrderCode()
    {
        var rnd = new Random();
        return rnd.NextInt64(1_000_000_000, 9_999_999_999); // 10-digit positive
    }
}