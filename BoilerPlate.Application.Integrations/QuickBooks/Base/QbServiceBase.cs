using BoilerPlate.Application.Integrations.Base;
using BoilerPlate.Application.Integrations.QuickBooks.Authorization;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace BoilerPlate.Application.Integrations.QuickBooks.Base
{
    public class QbServiceBase<TRequest, TResponse> : IntegrationServiceBase, IQbServiceBase<TRequest, TResponse>
    {
        protected readonly IQuickBooksConnectService _quickBooksConnectService;
        protected readonly string _endpoint;

        public QbServiceBase(HttpClient apiClient, IQuickBooksConnectService quickBooksConnectService, string endpoint)
        {
            _quickBooksConnectService = quickBooksConnectService;
            _endpoint = endpoint;

            ApiClient = apiClient;
            ApiClient.BaseAddress = new Uri(_quickBooksConnectService.QuickBooksSetting.BaseUrl);
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<TResponse> GetByAsync(long id)
        {
            try
            {
                await SetAuthHeader();

                return await GetAsync<TResponse>(string.Format("{0}/{1}?minorversion={2}", _endpoint, id, _quickBooksConnectService.QuickBooksSetting.MinorVersion));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<TResponse> CreateAsync(TRequest dto)
        {
            try
            {
                await SetAuthHeader();

                var result = await PostAsync(string.Format("{0}?minorversion={1}", _endpoint, _quickBooksConnectService.QuickBooksSetting.MinorVersion), dto);

                return JsonConvert.DeserializeObject<TResponse>(await result.Content.ReadAsStringAsync());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<TResponse> UpdateAsync(TRequest dto)
        {
            try
            {
                await SetAuthHeader();

                var result = await PostAsync(string.Format("{0}?minorversion={1}", _endpoint, _quickBooksConnectService.QuickBooksSetting.MinorVersion), dto);

                return JsonConvert.DeserializeObject<TResponse>(await result.Content.ReadAsStringAsync());
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region Protected Methods

        protected async Task SetAuthHeader()
        {
            var accessToken = await _quickBooksConnectService.GetAccessTokenAsync();

            ApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }

        #endregion Protected Methods
    }
}
