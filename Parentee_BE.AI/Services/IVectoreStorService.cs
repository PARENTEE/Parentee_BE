namespace Parentee_BE.AI.Services;

public interface IVectorStoreService
{
    Task<List<string>> GetCollection();
    
}