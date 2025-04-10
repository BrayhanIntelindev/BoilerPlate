using BoilerPlate.Domain.Entities.Settings.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerPlate.Domain.Entities.Settings.Dynamics
{
    public class MicrosoftDynamicsSettings: AppSettingsBase
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Authority { get; set; }
        public string ResourceUrl { get; set; }
        public string TenantId { get; set; }
        public string APIVersion { get; set; }
    }
}
