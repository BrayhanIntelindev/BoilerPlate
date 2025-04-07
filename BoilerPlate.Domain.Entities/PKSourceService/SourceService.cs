using BoilerPlate.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerPlate.Domain.Entities.PKSourceService
{
    public class SourceService : BaseEntity
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public string SourceServiceName { get; set; } = string.Empty;
        public string SourceServiceDescription { get; set; } = string.Empty;
        public string SourceFullUrl { get; set; } = string.Empty;
    }
}
