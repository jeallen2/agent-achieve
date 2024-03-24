using AgentAchieve.Core.Common;

namespace AgentAchieve.Infrastructure.Services;

/// <summary>
/// Represents a base service interface for CRUD operations on entities.
/// </summary>
/// <typeparam name="TEntity">The type of entity.</typeparam>
public interface IServiceBase<TEntity> where TEntity : class, IEntityPk
{
    /// <summary>
    /// Retrieves an entity by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the entity.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the retrieved entity, or null if not found.</returns>
    Task<TEntity?> GetByIdAsync(object id);

    /// <summary>
    /// Retrieves all entities. Optionally, includes related entities in the results.
    /// </summary>
    /// <param name="includes">A function to include related entities in the results. If null, no related entities are included.</param>
    /// <returns>An <see cref="IQueryable{TEntity}"/> representing the collection of entities.</returns>
    IQueryable<TEntity> GetAll(Func<IQueryable<TEntity>, IQueryable<TEntity>>? includes = null);

    /// <summary>
    /// Inserts an entity asynchronously.
    /// </summary>
    /// <param name="entity">The entity to insert.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task InsertAsync(TEntity entity);

    /// <summary>
    /// Updates an entity asynchronously.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task UpdateAsync(TEntity entity);

    /// <summary>
    /// Deletes an entity by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the entity to delete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task DeleteAsync(object id);

    // DTO Methods

    /// <summary>
    /// Retrieves a DTO (Data Transfer Object) by its ID asynchronously.
    /// </summary>
    /// <typeparam name="TDto">The type of DTO.</typeparam>
    /// <param name="id">The ID of the DTO.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the retrieved DTO, or null if not found.</returns>
    Task<TDto?> GetDtoByIdAsync<TDto>(object id) where TDto : class;

    /// <summary>
    /// Gets all DTOs.
    /// </summary>
    /// <typeparam name="TDto">The type of DTO.</typeparam>
    /// <param name="includes">A function to include related entities in the results. If null, no related entities are included.</param>
    /// <returns>An <see cref="IQueryable{TDto}"/> representing all DTOs.</returns>
    IQueryable<TDto> GetAllDto<TDto>(Func<IQueryable<TEntity>, IQueryable<TEntity>>? includes = null) where TDto : class;

    /// <summary>
    /// Inserts a DTO (Data Transfer Object) asynchronously.
    /// </summary>
    /// <typeparam name="TDto">The type of DTO.</typeparam>
    /// <param name="dto">The DTO to insert.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the inserted DTO.</returns>
    Task<TDto> InsertDtoAsync<TDto>(TDto dto) where TDto : class;

    /// <summary>
    /// Updates a DTO (Data Transfer Object) asynchronously.
    /// </summary>
    /// <typeparam name="TDto">The type of DTO.</typeparam>
    /// <param name="id">The ID of the DTO to update.</param>
    /// <param name="dto">The DTO to update.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the updated DTO.</returns>
    Task<TDto> UpdateDtoAsync<TDto>(object id, TDto dto) where TDto : class;
}