namespace BoilerPlate.Application.Integrations.QuickBooks.Base
{
    public interface IQbServiceBase<TRequest, TResponse>
    {
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TResponse> GetByAsync(long id);

        /// <summary>
        /// Create async
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<TResponse> CreateAsync(TRequest dto);

        /// <summary>
        /// Update async
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<TResponse> UpdateAsync(TRequest dto);
    }
}
