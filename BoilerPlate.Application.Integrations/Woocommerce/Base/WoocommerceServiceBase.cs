using System.Net.Http.Headers;
using BoilerPlate.Application.Integrations.Woocommerce.Authorization;
using BoilerPlate.Domain.Entities.Extensions;

namespace BoilerPlate.Application.Integrations.Woocommerce.Base
{
    public class WoocommerceServiceBase<TResponse, TParams> : IntegrationServiceBase , IWoocommerceServiceBase<TResponse, TParams>
    {
        private readonly IWoocommerceConnectService _woocomerceConnectService;
        protected readonly string _endpoint;

        public WoocommerceServiceBase(HttpClient apiClient, IWoocommerceConnectService woocommerceConnect, string endpoint)
        {
            _woocomerceConnectService = woocommerceConnect;
            _endpoint = endpoint;

            ApiClient = apiClient;
            ApiClient.BaseAddress = new Uri(_woocomerceConnectService.WooCommerceSetting.Url);
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        protected void SetAuthHeader()
        {
            var accessToken = _woocomerceConnectService.GenerateToken();
            ApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", accessToken);
        }

        public async Task<List<TResponse>> GetAllAsync()
        {
            SetAuthHeader();
            var result = await GetAsync<List<TResponse>>(_endpoint);
            if (result == null)
            {
                return [];
            }
            return result;
        }

        public async Task<List<TResponse>> GetAllAsync(TParams search)
        {
            SetAuthHeader();
            var queryString = search.ToQueryString();
            var result = await GetAsync<List<TResponse>>($"{_endpoint}?{queryString}");
            if (result == null)
            {
                return [];
            }
            return result;
        }

        public async Task<TResponse> GetByIdAsync(int id)
        {
            SetAuthHeader();
            var result = await GetAsync<TResponse>($"{_endpoint}/{id}");
            return result;
        }
    }
}
