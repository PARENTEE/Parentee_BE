using Parentee_BE.DAL.Data.RequestDTO.Ai;

namespace Parentee_BE.BLL.Services.Interfaces;

public interface IAiService
{
    Task<string> HandleChat(ChatRequestDTO chatRequestDto);

}