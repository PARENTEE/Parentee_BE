using System.Text.Json.Serialization;

namespace Parentee_BE.DAL.Data.Metadatas;

public class ApiResponse<TResponse>
{
    [JsonPropertyName("status_code")]
    public int StatusCode { get; set; }
    
    [JsonPropertyName("is_success")]
    public bool IsSuccess { get; set; }
    
    [JsonPropertyName("message")]
    public string Message { get; set; }
    
    [JsonPropertyName("reason")]
    public string? Reason { get; set; }
    
    [JsonPropertyName("data")]
    public TResponse? Data { get; set; }
}