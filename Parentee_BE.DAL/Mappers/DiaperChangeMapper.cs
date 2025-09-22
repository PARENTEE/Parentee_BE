using AutoMapper;
using Parentee_BE.DAL.Data.RequestDTO.DiaperChange;
using Parentee_BE.DAL.Data.ResponseDTO.DiaperChange;

namespace Parentee_BE.DAL.Mappers;

public class DiaperChangeMapper : Profile
{
    public DiaperChangeMapper()
    {
        // Request -> Entity
        CreateMap<CreateDiaperChangeRequest, DiaperChangeMapper>();
        CreateMap<UpdateDiaperChangeRequest, DiaperChangeMapper>();

        // Entity -> Response
        CreateMap<DiaperChangeMapper, GetDiaperChangeResponse>();
    }
}