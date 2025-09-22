using Parentee_BE.DAL.Data.RequestDTO.Measurement;
using Parentee_BE.DAL.Data.ResponseDTO.Measurement;

namespace Parentee_BE.BLL.Services.Interfaces;

public interface IMeasurementService
{
    Task<GetMeasurementResponse> GetMeasurementById(Guid id);
    Task<GetMeasurementResponse> CreateMeasurement(CreateMeasurementRequest requestDto);
    Task<GetMeasurementResponse> UpdateMeasurement(Guid id, UpdateMeasurementRequest requestDto);
    Task<bool> DeleteMeasurement(Guid id);
}