using BoilerPlate.Application.Integrations.Powertranz.Authorization;
using BoilerPlate.Application.Integrations.Woocommerce.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace BoilerPlate.Application.Integrations.Powertranz.Base
{
    public class PowertranzServiceBase: IntegrationServiceBase
    {
        private readonly IPowertranzConnectService _powertranzConnectService;

        public PowertranzServiceBase(HttpClient apiClient, IPowertranzConnectService powertranzConnectService)
        {
            _powertranzConnectService = powertranzConnectService;

            ApiClient = apiClient;
            ApiClient.BaseAddress = new Uri(_powertranzConnectService.PowerTranzUrl);
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task Alive()
        {
            var response = await ApiClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, nameof(Alive)));
        }
    }
}
