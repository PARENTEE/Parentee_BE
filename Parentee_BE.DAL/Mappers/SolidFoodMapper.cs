using AutoMapper;
using Parentee_BE.DAL.Data.Entities;
using Parentee_BE.DAL.Data.RequestDTO.SolidFood;
using Parentee_BE.DAL.Data.ResponseDTO.SolidFood;

namespace Parentee_BE.DAL.Mappers;

public class SolidFoodMapper : Profile
{
    public SolidFoodMapper()
    {
        // Request -> Entity
        CreateMap<CreateSolidFoodRequest, SolidFoodEntity>()
            .ForMember(dest => dest.AteAt,
                opt => opt.MapFrom(src => DateTime.SpecifyKind(src.AteAt, DateTimeKind.Utc)));
        // Entity -> Response
        CreateMap<SolidFoodEntity, GetSolidFoodResponse>();
    }
}