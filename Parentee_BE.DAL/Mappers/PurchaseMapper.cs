using AutoMapper;
using Parentee_BE.DAL.Data.Entities;
using Parentee_BE.DAL.Data.ResponseDTO.Payment;

namespace Parentee_BE.DAL.Mappers;

public class PurchaseMapper : Profile
{
    public PurchaseMapper()
    {
        // In your AutoMapper profile
        CreateMap<PurchaseModel, PurchaseEntity>()
            .ForMember(d => d.CreatedAt, opt => opt.Ignore())
            .ForMember(d => d.UpdatedAt, opt => opt.Ignore());

        CreateMap<PurchaseEntity, PurchaseModel>();
        
    }
}