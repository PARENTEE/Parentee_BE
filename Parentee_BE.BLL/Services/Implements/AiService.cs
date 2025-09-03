using Microsoft.Extensions.Logging;
using Parentee_BE.BLL.Services.Interfaces;
using Parentee_BE.DAL.Context;
using Parentee_BE.DAL.Data.Repositories.Interfaces;
using Parentee_BE.DAL.Data.RequestDTO.Ai;

namespace Parentee_BE.BLL.Services.Implements;

public class AiService(IUnitOfWork<AppDbContext> unitOfWork, ILogger<AiService> logger)
    : BaseService<AiService>(unitOfWork, logger), IAiService
{
    public Task<string> HandleChat(ChatRequestDTO chatRequestDto)
    {
        chatRequestDto.Message = "Hellop";
        throw new NotImplementedException();
    }
}