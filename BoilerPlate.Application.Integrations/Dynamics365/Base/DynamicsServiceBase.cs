
using System.Net.Http.Headers;
using System.Security.Policy;
using BoilerPlate.Application.Integrations.Base;
using BoilerPlate.Application.Integrations.Dynamics365.Authorization;
using BoilerPlate.Application.Integrations.Dynamics365.Dto;
using Newtonsoft.Json;

namespace BoilerPlate.Application.Integrations.Dynamics365.Base
{
    public class DynamicsServiceBase<T> : IntegrationServiceBase , IDynamicsServiceBase<T>
    {
        private readonly IDynamicsAuthorizationService _crmAuthorizationService;
        private readonly string _entityName;

        public DynamicsServiceBase(HttpClient apiClient, IDynamicsAuthorizationService crmAuthorizationService, string entityName)
        {
            ApiClient = apiClient;
            _crmAuthorizationService = crmAuthorizationService;
            _entityName = entityName;

            ApiClient.BaseAddress = new Uri(_crmAuthorizationService.Settings.ResourceUrl);
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        protected async Task SetAuthHeader()
        {
            var token = await _crmAuthorizationService.GetAccessTokenAsync();
            ApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<IEnumerable<T>> GetEntitiesAsync(string query = "")
        {
            await SetAuthHeader();
            var urlBase = $"/api/data/v{_crmAuthorizationService.Settings.APIVersion}/{_entityName}";

            if(!string.IsNullOrEmpty(query))
            {
                urlBase += $"?$filter=({query})";
            }
            var response = await ApiClient.GetAsync(urlBase);

            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<DynamicsListResponse<T>>(content);

            if(data == null)
            {
                return [];
            }

            return data.Value;
        }

        public async Task<T> GetEntityAsync(Guid id)
        {
            await SetAuthHeader();
            var response = await ApiClient.GetAsync($"/api/data/v{_crmAuthorizationService.Settings.APIVersion}/{_entityName}({id})");

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content);
        }

        public async Task<T> CreateEntityAsync(T entity)
        {
            await SetAuthHeader();
            var content = new StringContent(JsonConvert.SerializeObject(entity), System.Text.Encoding.UTF8, "application/json");
            var response = await PostAsync($"/api/data/v{_crmAuthorizationService.Settings.APIVersion}/{_entityName}", content);

            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseContent);
        }

        public async Task UpdateEntityAsync(Guid id, T entity)
        {
            await SetAuthHeader();
            var content = new StringContent(JsonConvert.SerializeObject(entity), System.Text.Encoding.UTF8, "application/json");
            var response = await PutAsync($"/api/data/v{_crmAuthorizationService.Settings.APIVersion}/{_entityName}({id})", content);

            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteEntityAsync(Guid id)
        {
            await SetAuthHeader();
            var response = await DeleteAsync($"/api/data/v{_crmAuthorizationService.Settings.APIVersion}/{_entityName}({id})");

            response.EnsureSuccessStatusCode();
        }
    }
}
