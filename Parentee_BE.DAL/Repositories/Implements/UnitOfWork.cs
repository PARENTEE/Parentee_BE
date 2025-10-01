using Parentee_BE.DAL.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Parentee_BE.DAL.Data.Repositories;

public class UnitOfWork<TContext> : IUnitOfWork<TContext>, IAsyncDisposable where TContext : DbContext
{
    public TContext Context { get; }    
    private Dictionary<Type, object> _repositories;

    public UnitOfWork(TContext context)
    {
        Context = context;
    }

    #region Repository Management
    
    public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class
    {
        _repositories ??= new Dictionary<Type, object>();
        if (_repositories.TryGetValue(typeof(TEntity), out var repository))
        {
            return (IGenericRepository<TEntity>)repository;
        }
        repository = new GenericRepository<TEntity>(Context);
        _repositories.Add(typeof(TEntity), repository);
        return (IGenericRepository<TEntity>)repository;
    }

    #endregion

    #region Transaction Management
    
    public async Task<T> ExecuteInTransactionAsync<T>(Func<Task<T>> operation)
    {
        // Retry transaction if failed
        // ref: https://learn.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency
        var executionStrategy = Context.Database.CreateExecutionStrategy();
        
        return await executionStrategy.ExecuteAsync(async () =>
        {
            await using var transaction = Context.Database.BeginTransaction();
            try
            {
                // Return result of the function
                var result = await operation();
                await Context.SaveChangesAsync(); // Automatically handle validation
                await transaction.CommitAsync();
                return result;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        });
    }

    public int SaveChanges()
    {
        return Context.SaveChanges();
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await Context.SaveChangesAsync(cancellationToken);
    }

    public async Task ExecuteInTransactionAsync(Func<Task> operation)
    {
        var executionStrategy = Context.Database.CreateExecutionStrategy();
        await executionStrategy.ExecuteAsync(async () =>
        {
            await using var transaction = Context.Database.BeginTransaction();
            try
            {
                await operation();
                await Context.SaveChangesAsync(); // Automatically handle validation
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        });
    }
    
    #endregion


    #region IDisposable implementation
    
    public void Dispose()
    {
        Context?.Dispose();
        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync()
    {
        await Context.DisposeAsync();
        GC.SuppressFinalize(this);
    }

    #endregion
    
}