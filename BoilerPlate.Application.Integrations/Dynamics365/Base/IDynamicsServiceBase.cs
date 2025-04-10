namespace BoilerPlate.Application.Integrations.Dynamics365.Base
{
    public interface IDynamicsServiceBase<T>
    {
        /// <summary>
        /// Gets a list of entities.
        /// </summary>
        /// <param name="query">
        /// query string in format: field1 eq 'value1' and field2 eq true and field3 eq 1
        /// </param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetEntitiesAsync(string query);

        /// <summary>
        /// Gets an entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the entity.</param>
        /// <returns>The entity.</returns>
        Task<T> GetEntityAsync(Guid id);

        /// <summary>
        /// Creates a new entity.
        /// </summary>
        /// <param name="entity">The entity to create.</param>
        /// <returns>The created entity.</returns>
        Task<T> CreateEntityAsync(T entity);

        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <param name="id">The ID of the entity to update.</param>
        /// <param name="entity">The updated entity.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task UpdateEntityAsync(Guid id, T entity);

        /// <summary>
        /// Deletes an entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the entity to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task DeleteEntityAsync(Guid id);
    }
}
