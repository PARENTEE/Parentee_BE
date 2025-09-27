using Parentee_BE.DAL.Data.Entities;
using Parentee_BE.DAL.Data.RequestDTO.Children;
using Parentee_BE.DAL.Data.ResponseDTO.Children;

namespace Parentee_BE.BLL.Services.Interfaces;

public interface IChildService
{
    Task<CreateChildResponseDTO> CreateChild(CreateChildRequestDTO request);
    Task<IEnumerable<CreateChildResponseDTO>> GetAllChildren();
    Task<CreateChildResponseDTO> GetChildById(Guid id);
    Task<GetChildTodayResponse> GetChildTodayById(Guid id);
    Task<CreateChildResponseDTO> UpdateChild(Guid id, CreateChildRequestDTO request);
    Task<bool> DeleteChild(Guid id);
}