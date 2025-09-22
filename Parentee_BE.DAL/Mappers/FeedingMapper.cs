using AutoMapper;
using Parentee_BE.DAL.Data.Entities;
using Parentee_BE.DAL.Data.RequestDTO;
using Parentee_BE.DAL.Data.ResponseDTO.Feedings;

namespace Parentee_BE.DAL.Mappers;

public class FeedingMapper : Profile
{
    public FeedingMapper()
    {
        // Request -> Entity
        CreateMap<CreateFeedingRequest, FeedingEntity>();

        // Entity -> Response
        CreateMap<FeedingEntity, GetFeedingResponse>();
    }
}