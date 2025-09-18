using Qdrant.Client;

namespace Parentee_BE.AI.Services;

public class QdrantVectorStoreService : IVectorStoreService
{
    private readonly QdrantClient _qdrantClient;

    public QdrantVectorStoreService(QdrantClient qdrantClient)
    {
        _qdrantClient = qdrantClient;
    }
    
    public Task<List<string>> GetCollection()
    {
        throw new NotImplementedException();
    }
}