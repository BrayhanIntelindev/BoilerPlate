using BoilerPlate.Application.Integrations.Woocommerce.Product;
using Microsoft.Extensions.Logging;
using ApplicationDto = BoilerPlate.Application.Dto;
using IntegrationDto = BoilerPlate.Application.Integrations.Woocommerce.Dto;

namespace BoilerPlate.Application.Woocommerce.Product
{
    public class ProductService(IWoocommerceProductService woocommerceProductService, GlobalizacionService globalizacionService, ILogger<ProductService> logger) : BaseService(globalizacionService, logger), IProductService
    {
        private readonly IWoocommerceProductService _woocommerceProductService = woocommerceProductService;
        public async Task<Response<ApplicationDto.Product.ProductResponse>> GetByIdAsync(int id)
        {
            var product = await _woocommerceProductService.GetByIdAsync(id);
            return TransformToAppResponse(product, ToDto);
        }

        public async Task<Response<ApplicationDto.Product.ProductResponse>> GetBySkuAsync(string sku)
        {
            var product = await _woocommerceProductService.GetBySkuAsync(sku);
            return TransformToAppResponse(product, ToDto);
        }

        public async Task<Response<List<ApplicationDto.Product.ProductResponse>>> GetAllAsync()
        {
            var products = await _woocommerceProductService.GetAllAsync();
            return TransformToAppResponseList(products, ToDto);
        }

        public async Task<Response<List<ApplicationDto.Product.ProductResponse>>> GetVariationsBySkuAsync(string sku)
        {
            var products = await _woocommerceProductService.GetAllVariationsAsync(sku);
            _logger.LogError("GetVariationsBySkuAsync");
            return TransformToAppResponseList(products, ToDto);
        }

        private static ApplicationDto.Product.ProductResponse ToDto(IntegrationDto.Product.ProductResponse x)
        {
            return new ApplicationDto.Product.ProductResponse
            {
                Id = x.Id,
                Name = x.Name,
                Sku = x.Sku,
            };
        }
    }
}
