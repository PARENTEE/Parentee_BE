using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Parentee_BE.BLL.Services.Interfaces;
using Parentee_BE.DAL.Context;
using Parentee_BE.DAL.Data.Entities;
using Parentee_BE.DAL.Data.Enums;
using Parentee_BE.DAL.Data.PaymentDTO;
using Parentee_BE.DAL.Data.Repositories.Interfaces;

namespace Parentee_BE.BLL.Services.Implements;

public class ProductService(
    IUnitOfWork<AppDbContext> unitOfWork, 
    ILogger<ProductEntity> logger,
    IHttpContextAccessor? httpContextAccessor,
    IMapper mapper)
    : BaseService<ProductEntity>(unitOfWork, logger, httpContextAccessor), IProductService
{
    public async Task<ProductEntity> GetFirstProduct()
    {
        return await _unitOfWork.GetRepository<ProductEntity>().FirstOrDefaultAsync(predicate: p => p.IsActive == true);
    }

    public async Task<ICollection<ProductEntity>> GetAllProduct()
    {
        return await _unitOfWork.GetRepository<ProductEntity>().GetListAsync();
    }

    public async Task<ProductEntity> GetProductById(Guid id)
    {
        var productRepo = _unitOfWork.GetRepository<ProductEntity>();
        var product = await productRepo.FirstOrDefaultAsync(predicate: d => d.Id == id);
        return product;
    }

    public async Task<ProductDataPayment?> GetProductAndPriceAsync(
    Guid productId, Guid? priceId = null, PriceType? priceType = null)
{
    var repo = _unitOfWork.GetRepository<ProductEntity>();

    return await repo.FirstOrDefaultAsync(
        selector: p => new ProductDataPayment
        {
            Id   = p.Id,
            Name = p.Name,

            PriceId = p.Prices
                .Where(pr => pr.IsActive && pr.DeletedAt == null)
                .OrderBy(pr =>
                    priceId.HasValue   && pr.Id == priceId.Value          ? 0 :
                    priceType.HasValue && pr.PriceType == priceType.Value ? 1 : 2)
                .ThenByDescending(pr => pr.CreatedAt)
                .Select(pr => pr.Id)
                .FirstOrDefault(),

            PriceType = p.Prices
                .Where(pr => pr.IsActive && pr.DeletedAt == null)
                .OrderBy(pr =>
                    priceId.HasValue   && pr.Id == priceId.Value          ? 0 :
                    priceType.HasValue && pr.PriceType == priceType.Value ? 1 : 2)
                .ThenByDescending(pr => pr.CreatedAt)
                .Select(pr => pr.PriceType)
                .FirstOrDefault(),

            Amount = p.Prices
                .Where(pr => pr.IsActive && pr.DeletedAt == null)
                .OrderBy(pr =>
                    priceId.HasValue   && pr.Id == priceId.Value          ? 0 :
                    priceType.HasValue && pr.PriceType == priceType.Value ? 1 : 2)
                .ThenByDescending(pr => pr.CreatedAt)
                .Select(pr => pr.Amount)
                .FirstOrDefault(),

            Currency = p.Prices
                .Where(pr => pr.IsActive && pr.DeletedAt == null)
                .OrderBy(pr =>
                    priceId.HasValue   && pr.Id == priceId.Value          ? 0 :
                    priceType.HasValue && pr.PriceType == priceType.Value ? 1 : 2)
                .ThenByDescending(pr => pr.CreatedAt)
                .Select(pr => pr.Currency)
                .FirstOrDefault()
        },
        predicate: p => p.Id == productId && p.IsActive && p.DeletedAt == null
    );
}
}