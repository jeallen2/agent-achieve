namespace AgentAchieve.Infrastructure.Services
{
    /// <summary>
    /// Represents a generic repository interface for CRUD operations on entities.
    /// </summary>
    /// <typeparam name="T">The type of entity.</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Deletes an entity asynchronously based on the specified ID.
        /// </summary>
        /// <param name="id">The ID of the entity to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task DeleteAsync(object id);

        /// <summary>
        /// Retrieves all entities.
        /// </summary>
        /// <returns>An IQueryable representing the collection of entities.</returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// Retrieves an entity asynchronously based on the specified ID.
        /// </summary>
        /// <param name="id">The ID of the entity to retrieve.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task<T> GetByIdAsync(object id);

        /// <summary>
        /// Inserts a new entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity to insert.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task InsertAsync(T entity);

        /// <summary>
        /// Updates an existing entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task UpdateAsync(T entity);
    }
}
