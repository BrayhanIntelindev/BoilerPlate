using BoilerPlate.Domain.Entities.Settings.Stripe;
using BoilerPlate.Domain.Entities.Settings.Woocommerce;
using Microsoft.Extensions.Options;

namespace BoilerPlate.Application.Integrations.Stripe.Authorization
{
    public class StripeConnectService(IOptions<StripeSetting> stripeSetting) : IStripeConnectService
    {
        public StripeSetting StripeSetting { get; set; } = stripeSetting.Value;
        public string GenerateToken()
        {
            string apiKey = StripeSetting.ApiKey;
            return apiKey;
        }
    }
}
