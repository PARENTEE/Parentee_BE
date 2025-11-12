using AutoMapper;
using Parentee_BE.DAL.Data.Entities;
using Parentee_BE.DAL.Data.Enums;
using Parentee_BE.DAL.Data.RequestDTO.Children;
using Parentee_BE.DAL.Data.ResponseDTO.Children;
using Parentee_BE.DAL.Helpers;

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
        
        // Report Response
        CreateMap<FeedingEntity, ReportTimeBlock>()
            .ForMember(dest => dest.Message, opt => opt.MapFrom(c => "Bé được cho bú."))
            .ForMember(dest => dest.StartTime, opt => opt.MapFrom(c => TimeZoneHelper.ToVietnamTime(c.StartedAt)))
            .ForMember(dest => dest.EndTime, opt => opt.MapFrom(c => TimeZoneHelper.ToVietnamTime(c.EndedAt.Value)));

        CreateMap<SolidFoodEntity, ReportTimeBlock>()
            .ForMember(dest => dest.Message, opt => opt.MapFrom(c => $"Bé được cho ăn {c.Name}."))
            .ForMember(dest => dest.StartTime, opt => opt.MapFrom(c => TimeZoneHelper.ToVietnamTime(c.AteAt)))
            .ForMember(dest => dest.EndTime,
                opt => opt.MapFrom(c => TimeZoneHelper.ToVietnamTime(c.AteAt.AddMinutes(15))));

        CreateMap<SleepEntity, ReportTimeBlock>()
            .ForMember(dest => dest.Message, opt => opt.MapFrom(c => $"Bé ngủ."))
            .ForMember(dest => dest.StartTime, opt => opt.MapFrom(c => TimeZoneHelper.ToVietnamTime(c.StartedAt)))
            .ForMember(dest => dest.EndTime, opt => opt.MapFrom(c => TimeZoneHelper.ToVietnamTime(c.EndedAt.Value)));

        CreateMap<DiaperChangeEntity, ReportTimeBlock>()
            .ForMember(dest => dest.Message, opt => opt.MapFrom(c => BuildDiaperMessage(c)))
            .ForMember(dest => dest.StartTime, opt => opt.MapFrom(c => TimeZoneHelper.ToVietnamTime(c.ChangedAt)))
            .ForMember(dest => dest.EndTime,
                opt => opt.MapFrom(c => TimeZoneHelper.ToVietnamTime(c.ChangedAt.AddMinutes(15))));

        CreateMap<ChildEntity, GetChildReportResponse>()
            .ForMember(dest => dest.Feedings, opt => opt.MapFrom(c => c.Feedings))
            .ForMember(dest => dest.SolidFood, opt => opt.MapFrom(c => c.SolidFood))
            .ForMember(dest => dest.Sleep, opt => opt.MapFrom(c => c.Sleeps))
            .ForMember(dest => dest.DiaperChanges, opt => opt.MapFrom(c => c.DiaperChanges));
    }

    private static string BuildDiaperMessage(DiaperChangeEntity c)
    {
        string diaperQuality = "", quantity = "", color = "", diaperWaste = "";

        switch (c.Type)
        {
            case DiaperType.Dry: diaperQuality = "tã khô"; break;
            case DiaperType.Pee: diaperQuality = "tiểu"; break;
            case DiaperType.Poo: diaperQuality = "phân"; break;
            case DiaperType.Both: diaperQuality = "tiểu và phân"; break;
        }

        switch (c.DiaperQuantity)
        {
            case DiaperQuantity.Small: quantity = "nhỏ"; break;
            case DiaperQuantity.Medium: quantity = "vừa"; break;
            case DiaperQuantity.Large: quantity = "lớn"; break;
        }

        if (!string.IsNullOrEmpty(c.Color))
            color = $"màu {c.Color}";

        switch (c.DiaperWaste)
        {
            case DiaperWaste.Solid: diaperWaste = "cứng"; break;
            case DiaperWaste.Loose: diaperWaste = "lỏng"; break;
            case DiaperWaste.Runny: diaperWaste = "nước"; break;
            case DiaperWaste.Mucusy: diaperWaste = "nhầy"; break;
        }

        var parts = new List<string>();
        if (!string.IsNullOrEmpty(quantity)) parts.Add($"lượng {quantity}");
        if (!string.IsNullOrEmpty(color)) parts.Add(color);
        if (!string.IsNullOrEmpty(diaperWaste)) parts.Add($"chất {diaperWaste}");

        return $"Bạn thay tã do {diaperQuality}, {string.Join(", ", parts)}.";
    }
}