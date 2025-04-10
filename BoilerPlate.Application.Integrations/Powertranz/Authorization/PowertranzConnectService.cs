using BoilerPlate.Domain.Entities.Settings.Powertranz;
using BoilerPlate.Domain.Entities.Settings.Woocommerce;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerPlate.Application.Integrations.Powertranz.Authorization
{
    internal class PowertranzConnectService(IOptions<PowertranzSettings> powertranzSettings) : IPowertranzConnectService
    {
        public PowertranzSettings PowertranzSettings { get; set; } = powertranzSettings.Value;
        public string PowerTranzUrl { get; } = string.Format("https://{0}.ptranz.com/Api/", powertranzSettings.Value.Environment);
    }
}
