using Newtonsoft.Json;

namespace BoilerPlate.Application.Integrations.QuickBooks.Dto.Auth
{
    public class Token
    {
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty("x_refresh_token_expires_in")]
        public double RefreshExpiration { get; set; }

        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expires_in")]
        public double AccessExpiration { get; set; }
    }
}
