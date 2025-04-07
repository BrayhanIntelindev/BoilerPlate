using BoilerPlate.Application.Dto.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerPlate.Application.Woocommerce.Product
{
    public interface IProductService
    {
        Task<Response<List<ProductResponse>>> GetAllAsync();
        Task<Response<ProductResponse>> GetByIdAsync(int id);
        Task<Response<ProductResponse>> GetBySkuAsync(string sku);
        Task<Response<List<ProductResponse>>> GetVariationsBySkuAsync(string sku);
    }
}
