using Parentee_BE.DAL.Data.RequestDTO.DiaperChange;
using Parentee_BE.DAL.Data.ResponseDTO.DiaperChange;

namespace Parentee_BE.BLL.Services.Interfaces;

public interface IDiaperChangeService
{
    Task<GetDiaperChangeResponse> GetDiaperChangeById(Guid id);

    Task<GetDiaperChangeResponse> CreateDiaperChange(CreateDiaperChangeRequest requestDto);

    Task<GetDiaperChangeResponse> UpdateDiaperChange(Guid id, UpdateDiaperChangeRequest requestDto);

    Task<bool> DeleteDiaperChange(Guid id);
}