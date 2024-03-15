using AgentAchieve.Core.Common;
using AgentAchieve.Infrastructure.Data;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;


namespace AgentAchieve.Infrastructure.Services;

/// <summary>
/// Represents a unit of work for managing database operations.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="UnitOfWork"/> class.
/// </remarks>
/// <param name="context">The application database context.</param>
/// <param name="logger">The logger for logging errors.</param>
public class UnitOfWork(ApplicationDbContext context, ILogger<UnitOfWork> logger) : IDisposable, IUnitOfWork
{
    private readonly ApplicationDbContext _context = context;
    private ConcurrentDictionary<Type, object> _repositories = new ConcurrentDictionary<Type, object>();
    private bool _disposed = false;
    private readonly ILogger<UnitOfWork> _logger = logger;

    /// <summary>
    /// Gets the repository for the specified entity type.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <returns>The repository for the specified entity type.</returns>
    public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntityPk
    {
        return (IRepository<TEntity>)_repositories.GetOrAdd(typeof(TEntity), t => new Repository<TEntity>(_context));
    }

    /// <summary>
    /// Commits the pending changes to the database asynchronously.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task CommitAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while committing changes");
            throw;
        }
    }

    /// <summary>
    /// Disposes the unit of work and releases any resources held by it.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Disposes the unit of work and releases any resources held by it.
    /// </summary>
    /// <param name="disposing">A flag indicating whether the method is called from the <see cref="Dispose"/> method.</param>
    [ExcludeFromCodeCoverage]
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                // Dispose any managed resources held by the UnitOfWork here
                _context.Dispose();

                // Dispose repositories if they implement IDisposable
                foreach (var repository in _repositories.Values)
                {
                    if (repository is IDisposable disposable)
                    {
                        disposable.Dispose();
                    }
                }
            }

            _disposed = true;
        }
    }
}