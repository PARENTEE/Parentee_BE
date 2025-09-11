using AutoMapper;
using Parentee_BE.DAL.Data.Entities;
using Parentee_BE.DAL.Data.RequestDTO.Children;
using Parentee_BE.DAL.Data.ResponseDTO.Children;

namespace Parentee_BE.DAL.Mappers;

public class ChildMapper : Profile
{
    public ChildMapper()
    {
        // Request -> Entity
        CreateMap<CreateChildRequestDTO, ChildEntity>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));

        // Entity -> Response
        CreateMap<ChildEntity, CreateChildResponseDTO>();
    }
}