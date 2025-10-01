using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using Net.PayOSHQ;
using Net.PayOSHQ.Types;
using Parentee_BE.BLL.Services.Interfaces;
using Parentee_BE.Constants;
using Parentee_BE.DAL.Data.Entities;
using Parentee_BE.DAL.Data.Enums;
using Parentee_BE.DAL.Data.Metadatas;
using Parentee_BE.DAL.Data.PaymentDTO;
using Parentee_BE.DAL.Data.ResponseDTO.Payment;

namespace Parentee_BE.API.Controllers;
[AllowAnonymous]
public class PaymentController(
    ILogger<PaymentController> _logger, 
    IPaymentService _paymentService , 
    PayOS _payOs, 
    IOptions<PayOSOptions> _payOpts, 
    IPurchaseService _purchaseService,
    IUserFamilyRoleService _userFamilyRoleService) : BaseController<PaymentController>(_logger)
{
    [HttpGet(APIEndpointsConstant.PaymentEndpoints.CREATE_LINK)]
    public async Task<IActionResult> CreatePaymnetLink(Guid productId, Guid priceId, Guid userId)
    {
        var paymentData = await _paymentService.GetPaymentData(productId, priceId);
        var res = await _payOs.createPaymentLink(paymentData);

        var userRole = await _userFamilyRoleService.GetFamilyIdFromByUserID(userId);
        var newPur = new PurchaseModel
        {
            OrderCode = paymentData.orderCode,
            ProductId = productId,
            Amount = paymentData.amount,
            UserId = userId,
            FamilyId = userRole.FamilyId,
            Status = PurchaseStatus.Pending,
            PriceId = priceId
        };

        await _purchaseService.CreatePurchase(newPur);
        

        return Ok(
        
            ApiResponseBuilder.BuildResponse(
                statusCode: StatusCodes.Status200OK,
                isSuccess: true,
                message: "Create Payment Link Successfully",
                data: res
            )
        );
    }
    
    
    [HttpPost(APIEndpointsConstant.PaymentEndpoints.WEB_HOOK + "/test")]
    public async Task<IActionResult> TestWebhook(long orderCodeinput)
    {
 

        var orderCode = orderCodeinput; 
        var purchase = await _purchaseService.GetPurchaseByOrderCode(orderCode);
        if (purchase is null) return NotFound();

        purchase.Status = PurchaseStatus.Paid;
        purchase.PaidAt = DateTime.UtcNow;
        await _purchaseService.UpdatePurchase(purchase);

        return Ok(new { message = "Test webhook executed", orderCode, newStatus = purchase.Status });
    }

    
    [AllowAnonymous]
    [HttpPost(APIEndpointsConstant.PaymentEndpoints.WEB_HOOK)]
    [Consumes("application/json")]
    public async Task<IActionResult> Webhook([FromBody] WebhookType body)
    {
        
        WebhookDataType verified;
        try
        {
            verified = _payOs.verifyPaymentWebhookData(body);
        }
        catch (Exception ex)
        {
            return Ok();
        }

        var orderCode = verified.orderCode;
        var purchase = await _purchaseService.GetPurchaseByOrderCode(orderCode);

        if (purchase is null)
        {
            return Ok();
        }

        if (purchase.Status is PurchaseStatus.Paid or PurchaseStatus.Canceled or PurchaseStatus.Failed)
        {
            _logger.LogInformation("Order {OrderCode} already finalized with status {Status}", orderCode, purchase.Status);
            return Ok();
        }

  
        var isSuccess =
            string.Equals(verified.code, "PAYMENT_SUCCESS", StringComparison.OrdinalIgnoreCase) ||
            string.Equals(verified.desc, "success", StringComparison.OrdinalIgnoreCase);

        if (isSuccess)
        {
            purchase.Status = PurchaseStatus.Paid;
            purchase.PaidAt = DateTime.UtcNow;
            await _purchaseService.UpdatePurchase(purchase);
        }
        else
        {
            purchase.Status = PurchaseStatus.Failed;
            await _purchaseService.UpdatePurchase(purchase);
            _logger.LogInformation("Order {OrderCode} set to FAILED (code={Code}, desc={Desc}, status={Status})",
                orderCode, verified.code, verified.desc);
        }

        return Ok();
    }
    
    
    private static long GenerateOrderCode()
    {
        var rnd = new Random();
        return rnd.NextInt64(1_000_000_000, 9_999_999_999); // 10-digit positive
    }
}