using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerPlate.Application.Integrations.Woocommerce.Dto.Product
{
    public class ProductSearchRequest
    {
        public string? Name { get; set; } = null;
        public string? Sku { get; set; } = null;
    }
}
