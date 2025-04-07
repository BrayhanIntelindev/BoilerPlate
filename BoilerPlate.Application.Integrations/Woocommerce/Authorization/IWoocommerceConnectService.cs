using BoilerPlate.Domain.Entities.Settings.Woocommerce;

namespace BoilerPlate.Application.Integrations.Woocommerce.Authorization
{
    public interface IWoocommerceConnectService
    {
        #region BL
       
        WooCommerceSettings WooCommerceSetting { get; set; }
       
        #endregion
        
        /// <summary>
        /// AuthenticateUserAsync
        /// </summary>
        /// <returns></returns>
        string GenerateToken();

    }
}

