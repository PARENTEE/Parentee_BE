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
        var productRepo = _unitOfWork.GetRepository<ProductEntity>();
        var priceRepo = _unitOfWork.GetRepository<PriceEntity>();

        // 1) Lấy product cơ bản
        var product = await productRepo.FirstOrDefaultAsync(
            selector: p => new { p.Id, p.Name },
            predicate: p => p.Id == productId && p.IsActive && p.DeletedAt == null
        );

        if (product == null)
            return null;

        // 2) Chuẩn bị orderBy cho price dựa trên ưu tiên
        Func<IQueryable<PriceEntity>, IOrderedQueryable<PriceEntity>> orderBy = q =>
        {
            if (priceId.HasValue)
            {
                // Đưa price có Id == priceId lên hàng đầu, sau đó lấy newest
                return q.OrderBy(pr => pr.Id == priceId.Value ? 0 : 1)
                        .ThenByDescending(pr => pr.CreatedAt);
            }

            if (priceType.HasValue)
            {
                // Đưa price có PriceType trùng lên đầu, sau đó newest
                return q.OrderBy(pr => pr.PriceType == priceType.Value ? 0 : 1)
                        .ThenByDescending(pr => pr.CreatedAt);
            }

            // Không có preference => newest
            return q.OrderByDescending(pr => pr.CreatedAt);
        };

        // 3) Lấy price phù hợp (chỉ 1 bản ghi)
        var selectedPrice = await priceRepo.FirstOrDefaultAsync(
            selector: pr => new
            {
                pr.Id,
                pr.PriceType,
                pr.Amount,
                pr.Currency
            },
            predicate: pr => pr.ProductId == productId && pr.IsActive && pr.DeletedAt == null,
            orderBy: orderBy
        );

        // 4) Map ra ProductDataPayment
        var result = new ProductDataPayment
        {
            Id = product.Id,
            Name = product.Name,
            PriceId =  selectedPrice.Id,
            PriceType = selectedPrice.PriceType,
            Amount = selectedPrice?.Amount ?? 0m,
            Currency = selectedPrice?.Currency
        };

        return result;
    }


}