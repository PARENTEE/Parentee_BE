using Qdrant.Client.Grpc;

namespace Parentee_BE.AI.Services;

public interface IVectorStoreService
{
    Task<List<string>> GetCollectionList();

    Task<CollectionInfo> GetCollectionInfo(string collectionName);

    Task CreateCollection(string collectionName, ulong dimension);

    Task DeleteCollection(string collectionName);

    Task CreatePoint(string collectionName, string text);
    Task<RetrievedPoint?> GetPoint(string collectionName, ulong id);
}