using Parentee_BE.DAL.Data.RequestDTO.Family;
using Parentee_BE.DAL.Data.ResponseDTO.Family;

namespace Parentee_BE.BLL.Services.Interfaces;

public interface IFamilyService
{
    Task<GetFamilyResponse> GetFamilyById(Guid id);
    Task<GetFamilyDetailResponse> GetFamilyDetailById(Guid id);
    Task<GetFamilyResponse> CreateFamily(CreateFamilyRequest requestDto);
    Task<GetFamilyResponse> UpdateFamily(Guid id, UpdateFamilyRequest requestDto);
    Task<bool> DeleteFamily(Guid id);
}