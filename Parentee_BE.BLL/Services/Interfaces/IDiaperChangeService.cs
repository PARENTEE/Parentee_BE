using Parentee_BE.DAL.Data.Entities;
using Parentee_BE.DAL.Data.RequestDTO.DiaperChange;
using Parentee_BE.DAL.Data.ResponseDTO.DiaperChange;

public interface IDiaperChangeService
{
    Task<CreateDiaperChangeResponse?> CreateDiaperChange(CreateDiaperChangeRequest request);
    Task<UpdateDiaperChangeResponse?> UpdateDiaperChange(Guid childId, UpdateDiaperChangeRequest request);
    Task<CreateDiaperChangeResponse?> GetChildIdByDiaperChanges(Guid childId);
}