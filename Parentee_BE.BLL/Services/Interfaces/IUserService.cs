using Parentee_BE.DAL.Data.Entities;
using Parentee_BE.DAL.Data.RequestDTO.Users;
using Parentee_BE.DAL.Data.ResponseDTO.Users;

namespace Parentee_BE.BLL.Services.Interfaces;

public interface IUserService
{
    Task<UserEntity> GetUser();
    Task<GetUserResponseDTO> GetCurrentUser();
    Task<ICollection<GetUserResponseDTO>> GetManyUsers(int pageNumber, int pageSize);
    Task<GetUserResponseDTO> GetUserById(Guid id);
    Task<GetUserResponseDTO> CreateUser(CreateUserRequestDTO requestDto);
    Task<GetUserResponseDTO> UpdateUser(Guid id, UpdateUserRequestDTO requestDto);
    Task<bool> DeleteUser(Guid id);
}