using BoilerPlate.Domain.Entities.PKSourceService;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerPlate.Domain.Entities.PKFields
{
    public class FieldMapping : BaseEntity
    {
        public long SourceServiceId { get; set; }
        public virtual SourceService SourceService { get; set; } = new SourceService();
        public string SourceField { get; set; } = string.Empty;
        public string DestinationField { get; set; } = string.Empty;
    }
}
