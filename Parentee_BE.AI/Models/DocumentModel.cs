using Microsoft.Extensions.VectorData;

namespace Parentee_BE.AI.Models;

public class DocumentModel
{
    [VectorStoreKey] public required Guid Key { get; set; }
    
    [VectorStoreData(IsFullTextIndexed = true, StorageName = "content")]
    public required string Content { get; set; }
    
    [VectorStoreVector(Dimensions: 3072, DistanceFunction = DistanceFunction.CosineSimilarity, IndexKind = IndexKind.Hnsw, StorageName = "vectors")]
    public ReadOnlyMemory<float>? Vectors { get; set; }
}