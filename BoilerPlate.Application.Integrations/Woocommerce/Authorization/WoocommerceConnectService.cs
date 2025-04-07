using System.Text;
using BoilerPlate.Domain.Entities.Settings.Woocommerce;
using Microsoft.Extensions.Options;

namespace BoilerPlate.Application.Integrations.Woocommerce.Authorization
{
    public class WoocommerceConnectService(IOptions<WooCommerceSettings> wooCommerceSettings) : IWoocommerceConnectService
    {

        public WooCommerceSettings WooCommerceSetting { get; set; } = wooCommerceSettings.Value;

        public string GenerateToken()
        {
            string consumerKey = WooCommerceSetting.Key;
            string consumerSecret = WooCommerceSetting.Secret;

            var authValue = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{consumerKey}:{consumerSecret}"));
            
            return authValue;
        }
    }
}

