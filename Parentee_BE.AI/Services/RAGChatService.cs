#pragma warning disable SKEXP0001

using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.VectorData;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.Google;
using Microsoft.SemanticKernel.PromptTemplates.Handlebars;
using Parentee_BE.AI.Arugments;
using Parentee_BE.AI.Models;
using Parentee_BE.AI.Prompts;
using Parentee_BE.BLL.Services;
using Parentee_BE.DAL.Context;
using Parentee_BE.DAL.Data.Entities;
using Parentee_BE.DAL.Data.Repositories.Interfaces;

namespace Parentee_BE.AI.Services;

public class RagChatService(
    IUnitOfWork<AppDbContext> unitOfWork,
    ILogger<RagChatService> logger,
    IHttpContextAccessor httpContextAccessor,
    VectorStore vectorStore,
    Kernel kernel,
    IChatCompletionService chatCompletionService,
    IEmbeddingGenerator<string, Embedding<float>> _textEmbeddingGenerator) : 
    BaseService<RagChatService>(unitOfWork, logger, httpContextAccessor)
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


    private async Task<List<VectorSearchResult<DocumentModel>>> HybridSearchData(string collectionName, string question)
    {
        // Generate embeddings
        var embeddings = await _textEmbeddingGenerator.GenerateAsync(question);

        var searchVector = embeddings.Vector;

        if (searchVector.IsEmpty)
            throw new InvalidOperationException("Generated embedding is empty or invalid.");

        // Perform hybrid search
        var collection = (IKeywordHybridSearchable<DocumentModel>) vectorStore.GetCollection<Guid, DocumentModel>(collectionName);
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

    public async Task<string> Answer(UserArgument userArgument, string question)
    {
        const string collectionName = "parentee_docs";
        
        // Find related Information
        var searchResultList = await HybridSearchData(collectionName, question);
        
        // Add prompt template
        var arguments = ParenteePrompt.CreatePromptArguments(userArgument, question, searchResultList);
        
        var promptTemplateConfig = new PromptTemplateConfig
        {
            Template = ParenteePrompt.GetPromptTemplate(),
            TemplateFormat = "handlebars",
            Name = "ParenteeChatPrompt",
            AllowDangerouslySetContent = true, 
            ExecutionSettings = new Dictionary<string, PromptExecutionSettings>
            {
                {
                    PromptExecutionSettings.DefaultServiceId,
                    new PromptExecutionSettings
                    {
                        FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
                    }
                }
            }
        };
        
        // Invoke the prompt function
        var function = kernel.CreateFunctionFromPrompt(
            promptTemplateConfig, 
            new HandlebarsPromptTemplateFactory()); 
        var templateResponse = await kernel.InvokeAsync(function, arguments);
        
        return templateResponse.ToString();
    }

    public async Task<string> ChatAnswer(string question)
    {
        var userId = GetCurrentAccountIdThroughToken();
        
        var userEntity = await unitOfWork.GetRepository<UserEntity>()
            .FirstOrDefaultAsync(predicate: u => u.Id == userId,
                include: q => q.Include(u => u.UserFamilyRole));
        
        // Set User Arguments
        var userArguments = new UserArgument()
        {
            UserId = userId,
            Name = userEntity.FullName,
            Email = userEntity.Email,
            Role = userEntity.UserFamilyRole.Role.ToString()
        };
        
        // Chat
        GeminiPromptExecutionSettings geminiPromptExecutionSettings = new()
        {
            ToolCallBehavior = GeminiToolCallBehavior.AutoInvokeKernelFunctions,
            FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
        };  

        var history = new ChatHistory();
        // history.AddSystemMessage(renderedPrompt);
        history.AddSystemMessage(ParenteePrompt.GetChatPrompt(userArguments));
        history.AddUserMessage(question);
   
        var chatResult = await chatCompletionService.GetChatMessageContentAsync(
            history, 
            geminiPromptExecutionSettings,
            kernel);
        Console.WriteLine("Assistant > " + chatResult);

        // var chatResult = await kernel.InvokePromptAsync(question);
        return chatResult.Content.Trim();
    }
}