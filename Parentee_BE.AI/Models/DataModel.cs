using Microsoft.Extensions.VectorData;

namespace Parentee_BE.AI.Models;

public class DataModel
{
    [VectorStoreKey]
    public required string Key { get; init; }
    
    [VectorStoreData(IsIndexed = true, StorageName = "title")]
    public required string Title { get; init; }
    
    [VectorStoreData(IsFullTextIndexed = true, StorageName = "text")]
    public required string Text { get; init; }
    
    [VectorStoreVector(Dimensions: 512, DistanceFunction = DistanceFunction.CosineSimilarity, IndexKind = IndexKind.Hnsw)]
    public ReadOnlyMemory<float>? TextEmbedding { get; set; }
}