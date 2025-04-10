using BoilerPlate.Domain.Entities.Settings.Dynamics;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;

namespace BoilerPlate.Application.Integrations.Dynamics365.Authorization
{
    public class DynamicsAuthorizationService : IDynamicsAuthorizationService
    {
        public MicrosoftDynamicsSettings Settings { get; set; }
        private string _accessToken;
        private DateTimeOffset _tokenExpiration;

        public DynamicsAuthorizationService(IOptions<MicrosoftDynamicsSettings> settings)
        {
            Settings = settings.Value;
        }

        public async Task<string> GetAccessTokenAsync()
        {
            if (_accessToken == null || DateTimeOffset.UtcNow >= _tokenExpiration)
            {
                var app = ConfidentialClientApplicationBuilder.Create(Settings.ClientId)
                    .WithClientSecret(Settings.ClientSecret)
                    .WithAuthority(new Uri($"{Settings.Authority}/{Settings.TenantId}"))
                    .Build();

                var scopes = new string[] { $"{Settings.ResourceUrl}/.default" };

                var result = await app.AcquireTokenForClient(scopes).ExecuteAsync();
                _accessToken = result.AccessToken;
                _tokenExpiration = result.ExpiresOn.AddSeconds(-30);
            }

            return _accessToken;
        }
    }
}
