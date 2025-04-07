using BoilerPlate.Domain.Entities.Settings.Stripe;
using BoilerPlate.Domain.Entities.Settings.Woocommerce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerPlate.Application.Integrations.Stripe.Authorization
{
    public interface IStripeConnectService
    {
        #region BL

        StripeSetting StripeSetting { get; set; }

        #endregion
        /// <summary>
        /// AuthenticateUserAsync
        /// </summary>
        /// <returns></returns>
        string GenerateToken();
    }
}
