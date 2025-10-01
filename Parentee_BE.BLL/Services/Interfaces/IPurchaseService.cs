using Parentee_BE.DAL.Data.Entities;
using Parentee_BE.DAL.Data.ResponseDTO.Payment;

namespace Parentee_BE.BLL.Services.Interfaces;

public interface IPurchaseService
{
    Task<PurchaseModel> CreatePurchase(PurchaseModel purchase);
    
    Task<PurchaseModel> GetPurchaseByOrderCode(long orderCode);
    Task<PurchaseModel> UpdatePurchase(PurchaseModel purchase);
}