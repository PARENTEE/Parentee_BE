using System.Text.Json.Serialization;

namespace Parentee_BE.DAL.Data.Metadatas;

public class PagingResponse<T> 
{
    [JsonPropertyName("items")]
    public IEnumerable<T> Items { get; set; }

    [JsonPropertyName("meta")]
    public PaginationMeta Meta { get; set; }
}