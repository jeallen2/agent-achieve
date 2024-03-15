using AgentAchieve.Core.Common;

namespace AgentAchieve.Infrastructure.Services;

/// <summary>
/// Represents a generic repository interface for accessing and manipulating entities.
/// </summary>
/// <typeparam name="TEntity">The type of entity.</typeparam>
public interface IRepository<TEntity> where TEntity : class, IEntityPk
{
    /// <summary>
    /// Retrieves an entity by its unique identifier asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the entity.</param>
    /// <returns>The entity if found, otherwise null.</returns>
    Task<TEntity?> GetByIdAsync(object id);

    /// <summary>
    /// Retrieves all entities of the specified type.
    /// </summary>
    /// <returns>An IQueryable representing the collection of entities.</returns>
    IQueryable<TEntity> GetAll();

    /// <summary>
    /// Inserts a new entity asynchronously.
    /// </summary>
    /// <param name="entity">The entity to insert.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task InsertAsync(TEntity entity);

    /// <summary>
    /// Updates an existing entity.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    void Update(TEntity entity);

    /// <summary>
    /// Deletes an entity by its unique identifier asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the entity to delete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task DeleteAsync(object id);
}
