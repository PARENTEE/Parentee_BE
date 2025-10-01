using AutoMapper;
using Parentee_BE.DAL.Data.Entities;
using Parentee_BE.DAL.Data.RequestDTO.Measurement;
using Parentee_BE.DAL.Data.ResponseDTO.Measurement;

namespace Parentee_BE.DAL.Mappers;

public class MeasurementMapper : Profile
{
    public MeasurementMapper()
    {
        // Request -> Entity
        CreateMap<CreateMeasurementRequest, MeasurementEntity>();
        CreateMap<UpdateMeasurementRequest, MeasurementEntity>();

        // Entity -> Response
        CreateMap<MeasurementEntity, GetMeasurementResponse>();
    }
}