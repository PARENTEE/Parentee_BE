#pragma warning disable SKEXP0001

using System.Text.RegularExpressions;
using Microsoft.Extensions.VectorData;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Embeddings;
using Microsoft.SemanticKernel.PromptTemplates.Handlebars;
using Parentee_BE.AI.Arugments;
using Parentee_BE.AI.Models;
using Parentee_BE.AI.Prompts;

namespace Parentee_BE.AI.Services;

public class RAGChatService(
    VectorStore vectorStore,
    Kernel kernel,
    ITextEmbeddingGenerationService textEmbeddingGenerationService)
{
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


    private async Task<List<VectorSearchResult<DocumentVectorModel>>> HybridSearchData(string collectionName, string question)
    {
        // Generate embeddings
        var embeddings = await textEmbeddingGenerationService.GenerateEmbeddingsAsync([question],kernel);

        ReadOnlyMemory<float> searchVector = embeddings.FirstOrDefault();

        if (searchVector.IsEmpty)
            throw new InvalidOperationException("Generated embedding is empty or invalid.");

        // Perform hybrid search
        var collection = (IKeywordHybridSearchable<DocumentVectorModel>) vectorStore.GetCollection<ulong, DocumentVectorModel>(collectionName);
        var options = new HybridSearchOptions<DocumentVectorModel>
        {
            VectorProperty = r => r.TextEmbedding,
            AdditionalProperty = r => r.Text,
        };

        var keywords = ExtractKeywords(question);
        var searchResult = collection.HybridSearchAsync(
            searchVector,
            keywords,
            3,
            options);

        return await EnumeratorToList(searchResult);
    }

    public async Task<string> Answer(UserArgument userArugments, string question)
    {
        const string collectionName = "parentee";
        
        // Find related Information
        var searchResultList = await HybridSearchData(collectionName, question);
        
        // Add prompt template
        var arguments = ClaimRequestPrompt.CreatePromptArugments(userArugments, question, searchResultList);
        
        var promptTemplateConfig = new PromptTemplateConfig
        {
            Template = ClaimRequestPrompt.GetPromptTemplate(),
            TemplateFormat = "handlebars",
            Name = "ClaimRequestChatPrompt",
        };
        
        // Invoke the prompt function
        var function = kernel.CreateFunctionFromPrompt(
            promptTemplateConfig, 
            new HandlebarsPromptTemplateFactory()); 
        var templateResponse = await kernel.InvokeAsync(function, arguments);
        
        return templateResponse.ToString();
    }
}