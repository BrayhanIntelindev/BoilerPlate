using BoilerPlate.Domain.Entities.Settings.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerPlate.Domain.Entities.Settings.Woocommerce
{
    public class WooCommerceSettings: AppSettingsBase
    {
        public string Url { get; set; }
        public string Key { get; set; }
        public string Secret { get; set; }
    }
}