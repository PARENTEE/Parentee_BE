using Parentee_BE.DAL.Data.Entities;

namespace Parentee_BE.BLL.Services.Interfaces;

public interface IPriceService
{
    Task<PriceEntity> GetPrice();
}