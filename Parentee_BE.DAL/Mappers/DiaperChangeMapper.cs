using AutoMapper;
using Parentee_BE.DAL.Data.Entities;
using Parentee_BE.DAL.Data.RequestDTO.DiaperChange;
using Parentee_BE.DAL.Data.ResponseDTO.DiaperChange;

namespace Parentee_BE.DAL.Mappers;

public class DiaperChangeMapper : Profile
{
    public DiaperChangeMapper()
    {
        // Request -> Entity
        CreateMap<CreateDiaperChangeRequest, DiaperChangeEntity>()
            .ForMember(dest => dest.ChangedAt,
                opt => opt.MapFrom(src => DateTime.SpecifyKind(src.ChangedAt, DateTimeKind.Utc)));
        CreateMap<UpdateDiaperChangeRequest, DiaperChangeEntity>();

        // Entity -> Response
        CreateMap<DiaperChangeEntity, GetDiaperChangeResponse>();
    }
}