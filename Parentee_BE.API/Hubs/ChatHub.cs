using Microsoft.AspNetCore.SignalR;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.Google;
using Parentee_BE.AI.Prompts;
using Parentee_BE.AI.Services;

namespace Parentee_BE.API.Hubs;

public class ChatMessage
{
    public string Role { get; set; } // "user" hoặc "assistant"
    public string Content { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}

public static class ChatPool
{
    // Lưu history theo ConnectionId hoặc UserId
    public static Dictionary<string, List<ChatMessage>> Sessions = new();
}

public class ChatHub : Hub
{
    private readonly IChatCompletionService _chatService;
    private readonly Kernel _kernel;

    // Giữ pool các ChatHistory theo ConnectionId
    private static readonly Dictionary<string, ChatHistory> _sessions = new();

    public ChatHub(IChatCompletionService chatService, Kernel kernel)
    {
        _chatService = chatService;
        _kernel = kernel;
    }

    public override Task OnConnectedAsync()
    {
        // ✅ Khi user connect lần đầu → tạo ChatHistory mới
        _sessions[Context.ConnectionId] = new ChatHistory();
        // _sessions[Context.ConnectionId].AddSystemMessage(ParenteePrompt.GetChatPrompt("",Guid.Parse("f6f63db7-96e6-450b-bdbf-51e45bbdb171")));
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        // ✅ Khi user disconnect → xoá khỏi pool
        _sessions.Remove(Context.ConnectionId);
        return base.OnDisconnectedAsync(exception);
    }

    public async Task SendMessage(string message)
    {
        var connId = Context.ConnectionId;
        var history = _sessions[connId];

        // 👤 Thêm message người dùng
        history.AddUserMessage(message);

        // 🧠 Gọi AI với lịch sử hội thoại
        GeminiPromptExecutionSettings geminiPromptExecutionSettings = new()
        {
            ToolCallBehavior = GeminiToolCallBehavior.AutoInvokeKernelFunctions,
            FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
        };  
        var result = await _chatService.GetChatMessageContentAsync(
            history,
            geminiPromptExecutionSettings,
            _kernel
        );

        // 🤖 Thêm phản hồi AI vào lịch sử
        history.AddAssistantMessage(result.Content);

        // 📡 Gửi phản hồi lại client
        await Clients.Caller.SendAsync("ReceiveMessage", result.Content);
    }
}