using Parentee_BE.DAL.Data.RequestDTO;
using Parentee_BE.DAL.Data.RequestDTO.Feedings;
using Parentee_BE.DAL.Data.ResponseDTO.Feedings;

namespace Parentee_BE.BLL.Services.Interfaces;

public interface IFeedingService
{
    Task<GetFeedingResponse> GetFeedingById(Guid id);
    Task<GetFeedingResponse> CreateFeeding(CreateFeedingRequest requestDto);
    Task<GetFeedingResponse> UpdateFeeding(Guid id, UpdateFeedingRequest requestDto);
    Task<bool> DeleteFeeding(Guid id);
}