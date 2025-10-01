using AutoMapper;
using Microsoft.Extensions.Logging;
using Parentee_BE.BLL.Services.Interfaces;
using Parentee_BE.DAL.Context;
using Parentee_BE.DAL.Data.Entities;
using Parentee_BE.DAL.Data.Enums;
using Parentee_BE.DAL.Data.Repositories.Interfaces;
using Parentee_BE.DAL.Data.ResponseDTO.Payment;

namespace Parentee_BE.BLL.Services.Implements;

public class PurchaseService(IUnitOfWork<AppDbContext> _unitOfWork, ILogger<PurchaseEntity> _logger, IMapper _mapper) : BaseService<PurchaseEntity>(_unitOfWork, _logger), IPurchaseService
{
    public async Task<PurchaseModel> CreatePurchase(PurchaseModel purchase)
    {
        var repo = _unitOfWork.GetRepository<PurchaseEntity>();

        var newPur = _mapper.Map<PurchaseEntity>(purchase);
        
        newPur.CreatedAt = DateTime.UtcNow;
        newPur.UpdatedAt = DateTime.UtcNow;
        newPur.Currency =  "VND";
        newPur.PaymentMethod = PaymentMethod.CreditCard;

        try
        {
            await repo.InsertAsync(newPur);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        
        
        return _mapper.Map<PurchaseModel>(newPur);
    }

    public async Task<PurchaseModel> GetPurchaseByOrderCode(long orderCode)
    {
        var repo = _unitOfWork.GetRepository<PurchaseEntity>();
        var purchaseEntity = await repo.FirstOrDefaultAsync(predicate: d => d.OrderCode == orderCode);

        return _mapper.Map<PurchaseModel>(purchaseEntity);
    }

    public async Task<PurchaseModel> UpdatePurchase(PurchaseModel purchase)
    {
        var repo = _unitOfWork.GetRepository<PurchaseEntity>();
        var purchaseEntity = _mapper.Map<PurchaseEntity>(purchase);
        
        repo.UpdateAsync(purchaseEntity);
        
        return purchase;
    }
}