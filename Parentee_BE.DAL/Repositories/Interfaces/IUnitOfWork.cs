using Microsoft.EntityFrameworkCore;

namespace Parentee_BE.DAL.Data.Repositories.Interfaces;

public interface IUnitOfWork : IGenericRepositoryFactory, IDisposable
{
    Task<T> ExecuteInTransactionAsync<T>(Func<Task<T>> action);

    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    
    Task ExecuteInTransactionAsync(Func<Task> action);
}

public interface IUnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
{
    TContext Context { get; }
}