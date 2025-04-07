using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerPlate.Domain.Entities.Settings.Stripe
{
    public class StripeSetting : AppSettingsBase
    {
        public string ApiKey { get; set; } = string.Empty;
    }
}
