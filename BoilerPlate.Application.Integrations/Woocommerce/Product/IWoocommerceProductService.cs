using BoilerPlate.Application.Integrations.Woocommerce.Base;
using BoilerPlate.Application.Integrations.Woocommerce.Dto.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerPlate.Application.Integrations.Woocommerce.Product
{
    public interface IWoocommerceProductService : IWoocommerceServiceBase<ProductResponse, ProductSearchRequest>
    {
        /// <summary>
        /// Get product by sku
        /// </summary>
        /// <param name="sku"></param>
        /// <returns></returns>
        Task<ProductResponse?> GetBySkuAsync(string sku);
        /// <summary>
        /// Get all variations of a product
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        Task<List<ProductResponse>> GetAllVariationsAsync(string sku);
    }
}
