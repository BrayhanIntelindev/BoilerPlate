using BoilerPlate.Application.Integrations.Woocommerce.Authorization;
using BoilerPlate.Application.Integrations.Woocommerce.Base;
using BoilerPlate.Application.Integrations.Woocommerce.Dto.Product;
using System.Net.Http.Json;


namespace BoilerPlate.Application.Integrations.Woocommerce.Product
{
    public class WoocommerceProductService(HttpClient apiClient, IWoocommerceConnectService woocommerceClient) : WoocommerceServiceBase<ProductResponse, ProductSearchRequest>(apiClient, woocommerceClient, "products"), IWoocommerceProductService
    {
        public async Task<ProductResponse?> GetBySkuAsync(string sku)
        {
            SetAuthHeader();
            var request = await ApiClient.GetAsync($"{_endpoint}?sku={sku}");
            try
            {
                var product = await request.Content.ReadFromJsonAsync<ProductResponse>();
                return product;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<ProductResponse>> GetAllVariationsAsync(string sku)
        {
            SetAuthHeader();
            var mainProduct = await GetBySkuAsync(sku);
            if (mainProduct != null)
            {
                var request = await ApiClient.GetAsync($"{_endpoint}/{mainProduct.Id}/variations");
                var variations = await request.Content.ReadFromJsonAsync<List<ProductResponse>>();
                if (variations != null && variations.Count != 0) return variations;
            }
            return [];
        }
    }
}
