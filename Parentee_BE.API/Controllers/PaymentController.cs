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
    IInvoiceService _invoiceService,
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

    [AllowAnonymous] [HttpPost(APIEndpointsConstant.PaymentEndpoints.WEB_HOOK)] [Consumes("application/json")]
    public async Task<IActionResult> WebhookHandler([FromBody] WebhookType body)
    {
        WebhookDataType verified;
        try
        {
            verified = _payOs.verifyPaymentWebhookData(body);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to verify payment webhook data. Raw body: {@Body}", body);
            // trả Ok để không keep retry from provider (tuỳ requirement: bạn có thể trả 400 để provider retry)
            return Ok();
        }

        var orderCode = verified.orderCode;
        var purchase = await _purchaseService.GetPurchaseByOrderCode(orderCode);

        if (purchase is null)
        {
            _logger.LogWarning("Webhook for unknown orderCode {OrderCode}", orderCode);
            return Ok();
        }

        // nếu đã finalize thì ignore
        if (purchase.Status is PurchaseStatus.Paid or PurchaseStatus.Canceled or PurchaseStatus.Failed)
        {
            _logger.LogInformation("Order {OrderCode} already finalized with status {Status}", orderCode, purchase.Status);
            return Ok();
        }

        var isSuccess =
            string.Equals(verified.code, "PAYMENT_SUCCESS", StringComparison.OrdinalIgnoreCase) ||
            string.Equals(verified.desc, "success", StringComparison.OrdinalIgnoreCase);

        // lưu raw payload để tiện debug/kiểm toán
        try
        {
            // Ghi raw payload (JSON) vào purchase.RawPayload nếu property có
            purchase.RawPayload = System.Text.Json.JsonSerializer.Serialize(body);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Unable to serialize raw payload for order {OrderCode}", orderCode);
        }

        // // Optional: nếu providerTxnId có trong verified, lưu vào purchase
        // if (!string.IsNullOrEmpty(verified.providerTxnId))
        // {
        //     purchase.ProviderTxnId = verified.providerTxnId;
        // }
        //
        // // Dùng transaction để đảm bảo purchase + invoice consistent
        // // Nếu bạn không có ApplicationDbContext, bạn có thể bỏ phần transaction và chỉ dùng services
        // using var txn = _db != null
        //     ? await _db.Database.BeginTransactionAsync()
        //     : null as Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction;

        try
        {
            if (isSuccess)
            {
                purchase.Status = PurchaseStatus.Paid;
                purchase.PaidAt = DateTime.UtcNow;
                purchase.UpdatedAt = DateTime.UtcNow;

                await _purchaseService.UpdatePurchase(purchase);

                // tạo invoice
                var invoice = new InvoiceEntity
                {
                    Id = Guid.NewGuid(),
                    // PurchaseId = purchase.Id,
                    InvoiceNo = GenerateInvoiceNo(), // helper bên dưới
                    IssuedAt = DateTime.UtcNow,
                    // BuyerName = GetSafeString(verified.buyerName),
                    // BuyerEmail = GetSafeString(verified.buyerEmail),
                    BuyerTaxCode = null,
                    AmountTotal = purchase.Amount,
                    Currency = purchase.Currency,
                    PdfImageId = null,
                    CreatedAt = DateTime.UtcNow
                };

                await _invoiceService.CreateInvoice(invoice);

                _logger.LogInformation("Order {OrderCode} marked PAID and invoice {InvoiceNo} created", orderCode, invoice.InvoiceNo);
            }
            else
            {
                // xử lý thất bại thanh toán
                purchase.Status = PurchaseStatus.Failed;
                purchase.UpdatedAt = DateTime.UtcNow;
                await _purchaseService.UpdatePurchase(purchase);

                // tạo một bản ghi invoice "khi thất bại" — invoice_no để null (index duy nhất cho invoice_no cho phép null)
                var failInvoice = new InvoiceEntity
                {
                    Id = Guid.NewGuid(),
                    // PurchaseId = purchase.Id,
                    InvoiceNo = null,
                    IssuedAt = null,
                    // BuyerName = GetSafeString(verified.buyerName),
                    // BuyerEmail = GetSafeString(verified.buyerEmail),
                    BuyerTaxCode = null,
                    AmountTotal = purchase.Amount,
                    Currency = purchase.Currency,
                    PdfImageId = null,
                    CreatedAt = DateTime.UtcNow
                };

                await _invoiceService.UpdateInvoice(failInvoice);

                _logger.LogInformation("Order {OrderCode} set to FAILED (code={Code}, desc={Desc})", orderCode, verified.code, verified.desc);
            }
            //
            // if (txn != null)
            //     await txn.CommitAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing webhook for order {OrderCode}. Marking as Failed", orderCode);

            // đảm bảo purchase được mark Failed nếu xảy ra lỗi trong quá trình xử lý
            try
            {
                purchase.Status = PurchaseStatus.Failed;
                purchase.UpdatedAt = DateTime.UtcNow;
                await _purchaseService.UpdatePurchase(purchase);
            }
            catch (Exception innerEx)
            {
                _logger.LogError(innerEx, "Also failed to update purchase status to Failed for order {OrderCode}", orderCode);
            }

            // if (txn != null)
            //     await txn.RollbackAsync();

            // trả Ok để provider không keep retry (tùy yêu cầu). Nếu muốn provider retry, return 500.
            return Ok();
        }

        return Ok();
    }

    // Helpers
    private static string GenerateInvoiceNo()
    {
        // format: INV-YYYYMMDD-<6 random chars>
        var date = DateTime.UtcNow.ToString("yyyyMMdd");
        var suffix = Guid.NewGuid().ToString("N").Substring(0, 6).ToUpperInvariant();
        return $"INV-{date}-{suffix}";
    }

    private static string? GetSafeString(object? value)
    {
        if (value == null) return null;
        var s = value.ToString();
        return string.IsNullOrWhiteSpace(s) ? null : s;
    }
    
    private static long GenerateOrderCode()
    {
        var rnd = new Random();
        return rnd.NextInt64(1_000_000_000, 9_999_999_999); // 10-digit positive
    }
}