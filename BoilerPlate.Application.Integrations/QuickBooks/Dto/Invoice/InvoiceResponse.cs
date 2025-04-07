using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerPlate.Application.Integrations.QuickBooks.Dto.Invoice
{
    public class InvoiceResponse
    {
        public Invoice Invoice { get; set; }

    }

    public class Invoice
    {
        public long Id { get; set; }
        public string DocNumber { get; set; }
        public CustomerRefInvoice CustomerRef { get; set; }
    }

    public class CustomerRefInvoice
    {
        [JsonProperty("value")]
        public string Value { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
