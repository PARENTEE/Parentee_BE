using Microsoft.Extensions.AI;
using Microsoft.Extensions.VectorData;
using Parentee_BE.AI.Models;
using Qdrant.Client;
using Qdrant.Client.Grpc;

namespace Parentee_BE.AI.Services;

public class QdrantVectorStoreService : IVectorStoreService
{
    private readonly VectorStore _vectorStore;
    private readonly QdrantClient _qdrantClient;
    private readonly IEmbeddingGenerator<string, Embedding<float>> _textEmbeddingGenerator;
    public QdrantVectorStoreService(VectorStore vectorStore, QdrantClient qdrantClient, IEmbeddingGenerator<string, Embedding<float>> textEmbeddingGenerator)
    {
        _vectorStore = vectorStore;
        _qdrantClient = qdrantClient;
        _textEmbeddingGenerator = textEmbeddingGenerator;
    }

    #region Collection

    public async Task<List<string>> GetCollectionList()
    {
        var collectionList = await _qdrantClient.ListCollectionsAsync();
        return collectionList.ToList();
    }

    public async Task<CollectionInfo> GetCollectionInfo(string collectionName)
    {
        return await _qdrantClient.GetCollectionInfoAsync(collectionName);
    }

    public async Task CreateCollection(string collectionName, ulong dimension)
    {
        await _qdrantClient.CreateCollectionAsync(
            collectionName: collectionName,
            vectorsConfig: new VectorParams
            {
                Size = dimension,
                Distance = Distance.Cosine
            }
        );

        // Full-text index for semantic / hybrid search
        await _qdrantClient.CreatePayloadIndexAsync(
            collectionName: collectionName,
            fieldName: "content",
            schemaType: PayloadSchemaType.Text,
            indexParams: new PayloadIndexParams
            {
                TextIndexParams = new TextIndexParams
                {
                    Tokenizer = TokenizerType.Word,
                    MinTokenLen = 2,
                    MaxTokenLen = 10,
                    Lowercase = true
                }
            }
        );
    }

    public async Task DeleteCollection(string collectionName)
    {
        await _qdrantClient.DeleteCollectionAsync(collectionName);
    }

    #endregion

    #region Points

    public async Task CreatePoint(string collectionName, string text)
    {
        var embedding = await _textEmbeddingGenerator.GenerateAsync(text);
        var collection = _vectorStore.GetCollection<ulong, DocumentVectorModel>(collectionName);

        var point = new DocumentVectorModel()
        {
            Key = BitConverter.ToUInt64(Guid.NewGuid().ToByteArray()),
            Title = "New Document",
            DocumentUri = "https://music.youtube.com/watch?v=BBj3SCImk_A",
            Text = text,
            TextEmbedding = embedding.Vector
        };
        await collection.UpsertAsync(point);
    }
    
    public async Task<RetrievedPoint?> GetPoint(string collectionName, ulong id)
    {
        var results = await _qdrantClient.RetrieveAsync(
            collectionName: collectionName,
            ids: [id],
            withPayload: false,
            withVectors: false
        );

        return results.FirstOrDefault();
    }

    #endregion

    #region Chat

    // public async Task<RetrievedPoint?> HybridSearch (string collectionName, ulong id)
    // {
    //     await _qdrantClient.QueryAsync(
    //         collectionName: collectionName,
    //         prefetch: new List < PrefetchQuery > {
    //             new() {
    //                 Query = new(float, uint)[] {
    //                     (0.22f, 1), (0.8f, 42),
    //                 },
    //                 Using = "sparse",
    //                 Limit = 20
    //             },
    //             new() {
    //                 Query = new float[] {
    //                     0.01f, 0.45f, 0.67f
    //                 },
    //                 Using = "dense",
    //                 Limit = 20
    //             }
    //         },
    //         query: Fusion.Rrf
    //     );
    //
    //     return results.FirstOrDefault();
    // }

    #endregion
    
    
}