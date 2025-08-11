using Parentee_BE.DAL.Data.Entities;
using Parentee_BE.DAL.Data.RequestDTO.Accounts;
using Parentee_BE.DAL.Data.ResponseDTO.Accounts;

namespace Parentee_BE.BLL.Services.Interfaces;

public interface IAccountService
{
    Task<AccountEntity> GetAccount();
    Task<GetAccountResponseDTO> GetCurrentAccount();
    Task<ICollection<GetAccountResponseDTO>> GetManyAccounts(int pageNumber, int pageSize);
    Task<GetAccountResponseDTO> GetAccountById(Guid id);
    Task<GetAccountResponseDTO> CreateAccount(CreateAccountRequestDTO requestDto);
    Task<GetAccountResponseDTO> UpdateAccount(Guid id, UpdateAccountRequestDTO requestDto);
    Task<bool> DeleteAccount(Guid id);
}