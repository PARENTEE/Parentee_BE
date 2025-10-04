using System.ComponentModel;
using System.Text.RegularExpressions;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.VectorData;
using Microsoft.SemanticKernel;
using Parentee_BE.AI.Models;

namespace Parentee_BE.AI.Plugins;

public class InternalDocumentsPlugin
{
    private IEmbeddingGenerator<string, Embedding<float>> _textEmbeddingGenerator;
    private VectorStore _vectorStore;

    public InternalDocumentsPlugin(IEmbeddingGenerator<string, Embedding<float>> textEmbeddingGenerator,  VectorStore vectorStore)
    {
        _textEmbeddingGenerator = textEmbeddingGenerator;
        _vectorStore = vectorStore;
    }
    
    private ICollection<string> ExtractKeywords(string query)
    {
        // Remove common words (stop words)
        string[] stopWords = { "what", "are", "the", "in", "on", "of", "is", "how" };
        string[] words = query.Split(' ');

        var keywords = words
            .Where(word => !stopWords.Contains(word.ToLower()))
            .Select(word => Regex.Replace(word, @"[^\w\s]", "")) // Remove punctuation
            .ToList();

        return keywords;
    }
     
    private async Task<List<VectorSearchResult<T>>> EnumeratorToList<T>(IAsyncEnumerable<VectorSearchResult<T>> asyncEnumerable)
    {
        var list = new List<VectorSearchResult<T>>();
        await foreach (var item in asyncEnumerable)
        {
            list.Add(item);
        }
        return list;
    }

    [KernelFunction("hybrid_search_data")]
    [Description("Performs a hybrid search on the knowledge base by combining vector similarity and keyword matching based on the user's question.")]
    [return: Description("A list of search results from the knowledge base, where each item contains a document and its relevance score, represented as List<VectorSearchResult<DocumentModel>>.")]
    public async Task<List<VectorSearchResult<DocumentModel>>> HybridSearchData(
        [Description("The natural language question or query provided by the user.")] 
        string question)
    {
        string collectionName = "parentee_docs";
        // Generate embeddings
        var embeddings = await _textEmbeddingGenerator.GenerateAsync(question);

        var searchVector = embeddings.Vector;

        if (searchVector.IsEmpty)
            throw new InvalidOperationException("Generated embedding is empty or invalid.");

        // Perform hybrid search
        var collection = (IKeywordHybridSearchable<DocumentModel>) _vectorStore.GetCollection<Guid, DocumentModel>(collectionName);
        var options = new HybridSearchOptions<DocumentModel>
        {
            VectorProperty = r => r.Vectors,
            // AdditionalProperty = r => r.Content,
        };

        var keywords = ExtractKeywords(question);
        var searchResult = collection.HybridSearchAsync(
            searchVector,
            keywords,
            3,
            options);

        return await EnumeratorToList(searchResult);
    }
}