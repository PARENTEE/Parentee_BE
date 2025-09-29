using Parentee_BE.DAL.Data.Entities;
using Parentee_BE.DAL.Data.Enums;
using Parentee_BE.DAL.Data.PaymentDTO;

namespace Parentee_BE.BLL.Services.Interfaces;

public interface IProductService
{
    Task<ProductEntity> GetFirstProduct();
    Task<ICollection<ProductEntity>> GetAllProduct();
    Task<ProductEntity> GetProductById(Guid id);

    Task<ProductDataPayment> GetProductAndPriceAsync(
        Guid productCode,
        Guid? priceId = null,
        PriceType? priceType = null);

}