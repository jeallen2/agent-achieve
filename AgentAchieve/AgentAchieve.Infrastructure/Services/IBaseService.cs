namespace AgentAchieve.Infrastructure.Services
{
    public interface IBaseService<T> where T : class
    {
        /// <summary>
        /// Deletes an entity by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the entity to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task DeleteAsync(int id);

        /// <summary>
        /// Deletes an entity by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the entity to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task DeleteAsync(string id);

        /// <summary>
        /// Retrieves an entity by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the entity to retrieve.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the retrieved entity.</returns>
        Task<T> GetByIdAsync(int id);

        /// <summary>
        /// Retrieves an entity by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the entity to retrieve.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the retrieved entity.</returns>
        Task<T> GetByIdAsync(string id);

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