using BoilerPlate.Domain.Entities.Settings.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerPlate.Domain.Entities.Settings.QuickBook
{
    public class QuickBooksSetting: AppSettingsBase
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string RedirectUrl { get; set; }
        public string Environment { get; set; }
        public string BaseUrl { get; set; }
        public int MinorVersion { get; set; }
    }
}
