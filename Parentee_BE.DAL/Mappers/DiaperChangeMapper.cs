using AutoMapper;
using Parentee_BE.DAL.Data.Entities;
using Parentee_BE.DAL.Data.Enums;
using Parentee_BE.DAL.Data.RequestDTO.DiaperChange;
using Parentee_BE.DAL.Data.ResponseDTO.DiaperChange;

namespace Parentee_BE.DAL.Mappers;

public class DiaperChangeMapper : Profile
{
    public DiaperChangeMapper()
    {
        // Request -> Entity
        CreateMap<CreateDiaperChangeRequest, DiaperChangeEntity>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.DeletedAt, opt => opt.Ignore());
        
        CreateMap<UpdateDiaperChangeRequest, DiaperChangeEntity>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.DeletedAt, opt => opt.Ignore());

        // Entity -> Response
        CreateMap<DiaperChangeEntity, CreateDiaperChangeResponse>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()));
        
        CreateMap<DiaperChangeEntity, UpdateDiaperChangeResponse>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()));
    }
}