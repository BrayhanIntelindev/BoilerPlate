namespace BoilerPlate.Application.Integrations.Woocommerce.Base
{
    public interface IWoocommerceServiceBase<TResponse, TParams>
    {
        /// <summary>
        /// Get all items from the endpoint
        /// </summary>
        /// <returns></returns>
        Task<List<TResponse>> GetAllAsync();
        /// <summary>
        /// Get all items from the endpoint by Params
        /// </summary>
        /// <returns></returns>
        Task<List<TResponse>> GetAllAsync(TParams search);
        /// <summary>
        /// Get item by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TResponse> GetByIdAsync(int id);
    }
}