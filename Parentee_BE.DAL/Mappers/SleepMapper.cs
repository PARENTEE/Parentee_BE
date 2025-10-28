using AutoMapper;
using Parentee_BE.DAL.Data.Entities;
using Parentee_BE.DAL.Data.RequestDTO.Sleep;
using Parentee_BE.DAL.Data.ResponseDTO.Sleep;

namespace Parentee_BE.DAL.Mappers;

public class SleepMapper : Profile
{
    public SleepMapper()
    {
        // Request -> Entity
        CreateMap<CreateSleepRequest, SleepEntity>()
            .ForMember(dest => dest.StartedAt,
                opt => opt.MapFrom(src => DateTime.SpecifyKind(src.StartTime, DateTimeKind.Utc)))
            .ForMember(dest => dest.EndedAt,
                opt => opt.MapFrom(src => DateTime.SpecifyKind(src.EndTime, DateTimeKind.Utc)));
        CreateMap<UpdateSleepRequest, SleepEntity>();

        // Entity -> Response
        CreateMap<SleepEntity, GetSleepResponse>();
    }
}