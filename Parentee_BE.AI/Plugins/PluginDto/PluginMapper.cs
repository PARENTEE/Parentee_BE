using AutoMapper;
using Parentee_BE.DAL.Data.Entities;

namespace Parentee_BE.AI.Plugins.PluginDto;

public class PluginMapper : Profile
{
    public PluginMapper()
    {
        // Child Plugins
        CreateMap<DiaperChangeEntity, DiaperChangeResponse>();
        CreateMap<FeedingEntity, FeedingResponse>();
        CreateMap<SleepEntity, SleepResponse>();

        CreateMap<ChildEntity, GetChildTodayForAiResponse>()
            .ForMember(dest => dest.SolidFoods, opt => opt.MapFrom(c => c.SolidFood))
            .ForMember(dest => dest.Feedings, opt => opt.MapFrom(c => c.Feedings))
            .ForMember(dest => dest.DiaperChanges, opt => opt.MapFrom(c => c.DiaperChanges))
            .ForMember(dest => dest.Sleeps, opt => opt.MapFrom(c => c.Sleeps));
    }
}