using BoilerPlate.Domain.Entities.Settings.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerPlate.Domain.Entities.Settings.Notifications
{
    public class NofiticationEmail: AppSettingsBase
    {
        public ReceptorProperty Admin { get; set; }
        public ReceptorProperty Manufacture { get; set; }
    }

    public class ReceptorProperty
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
