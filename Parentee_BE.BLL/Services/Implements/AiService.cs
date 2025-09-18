using Microsoft.Extensions.Logging;
using Parentee_BE.AI.Arugments;
using Parentee_BE.AI.Services;
using Parentee_BE.BLL.Services.Interfaces;
using Parentee_BE.DAL.Context;
using Parentee_BE.DAL.Data.Repositories.Interfaces;
using Parentee_BE.DAL.Data.RequestDTO.Ai;

namespace Parentee_BE.BLL.Services.Implements;

public class AiService(RAGChatService ragChatService, IUnitOfWork<AppDbContext> unitOfWork, ILogger<AiService> logger)
    : BaseService<AiService>(unitOfWork, logger), IAiService
{
    public async Task<string> HandleChat(ChatRequestDTO chatRequestDto)
    {
        var result = await ragChatService.Answer(
            new UserArgument()
            {
                Email = "newcustomer@gmail.com",
                Name = "Tran Viet Cuong",
                Role = "User"
            }, chatRequestDto.Message);
        
        return result;
    }
}