using Microsoft.Extensions.VectorData;

namespace Parentee_BE.AI.Models;

public class DocumentVectorModel
{
    [VectorStoreKey] public required ulong Key { get; set; } = BitConverter.ToUInt64(Guid.NewGuid().ToByteArray(), 0);
    
    [VectorStoreData(IsIndexed = true, StorageName = "title")]
    public required string Title { get; set; }
    
    [VectorStoreData(IsFullTextIndexed = true, StorageName = "text")]
    public required string Text { get; set; }
    
    [VectorStoreData(StorageName = "document_uri")]
    public required string DocumentUri { get; set; }
    
    [VectorStoreVector(Dimensions: 3072, DistanceFunction = DistanceFunction.CosineSimilarity, IndexKind = IndexKind.Hnsw, StorageName = "text_embedding")]
    public ReadOnlyMemory<float>? TextEmbedding { get; set; }
}