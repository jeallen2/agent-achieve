using AgentAchieve.Core.Common;

namespace AgentAchieve.Infrastructure.Services;
/// <summary>
/// Represents a unit of work for managing database transactions and repositories.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Commits the changes made in the unit of work to the underlying database.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task CommitAsync();

    /// <summary>
    /// Releases all resources used by the unit of work.
    /// </summary>
    void Dispose();

    /// <summary>
    /// Gets the repository for the specified entity type.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <returns>An instance of the repository for the specified entity type.</returns>
    IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntityPk;
}