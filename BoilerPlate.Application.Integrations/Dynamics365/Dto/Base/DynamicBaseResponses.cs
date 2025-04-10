using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerPlate.Application.Integrations.Dynamics365.Dto.Base
{
    public class DynamicsListResponse<T>
    {
        [JsonProperty("value")]
        public List<T> Value { get; set; } = [];
    }
}
