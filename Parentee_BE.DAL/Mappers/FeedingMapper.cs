using AutoMapper;
using Parentee_BE.DAL.Data.Entities;
using Parentee_BE.DAL.Data.RequestDTO;
using Parentee_BE.DAL.Data.RequestDTO.Feedings;
using Parentee_BE.DAL.Data.ResponseDTO.Feedings;

namespace Parentee_BE.DAL.Mappers;

public class FeedingMapper : Profile
{
    public FeedingMapper()
    {
        // Request -> Entity
        CreateMap<CreateFeedingRequest, FeedingEntity>()
            .ForMember(dest => dest.StartedAt, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.StartedAt, DateTimeKind.Utc)))
            .ForMember(dest => dest.RightDuration, opt => opt.MapFrom(src => src.RightDuration))
            .ForMember(dest => dest.LeftDuration, opt => opt.MapFrom(src => src.LeftDuration));
        CreateMap<UpdateFeedingRequest, FeedingEntity>();

        // Entity -> Response
        CreateMap<FeedingEntity, GetFeedingResponse>();
    }
}