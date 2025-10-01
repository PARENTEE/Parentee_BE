using Parentee_BE.DAL.Data.RequestDTO.Sleep;
using Parentee_BE.DAL.Data.ResponseDTO.Sleep;

namespace Parentee_BE.BLL.Services.Interfaces;

public interface ISleepService
{
    Task<GetSleepResponse> GetSleepById(Guid id);
    Task<GetSleepResponse> CreateSleep(CreateSleepRequest requestDto);
    Task<GetSleepResponse> UpdateSleep(Guid id, UpdateSleepRequest requestDto);
    Task<bool> DeleteSleep(Guid id);
}