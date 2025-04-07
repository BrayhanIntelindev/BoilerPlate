using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BoilerPlate.Application.Integrations.QuickBooks.Dto.Invoice
{
    public class InvoiceRequest
    {
        public List<Line> Line { get; set; }
        public CustomerRef CustomerRef { get; set; }
        public List<CustomField> CustomField { get; set; }
        public BillEmail BillEmail { get; set; }
        public string DueDate { get; set; }
        public BillAddr BillAddr { get; set; }
        public ShipAddr ShipAddr { get; set; }
    }

    public class Line
    {
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string DetailType { get; set; } = "SalesItemLineDetail";
        public SalesItemLineDetail SalesItemLineDetail { get; set; }
    }

    public class SalesItemLineDetail
    {
        public ItemRef ItemRef { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Qty { get; set; }
    }

    public class ItemRef
    {
        [JsonProperty("value")]
        public string Value { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class CustomerRef
    {
        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public class CustomField
    {
        public string DefinitionId { get; set; } = "1";
        public string StringValue { get; set; }
        public string Type { get; set; } = "StringType";
        public string Name { get; set; }
    }

    public class BillEmail
    {
        public string Address { get; set; }
    }

    public class BillAddr
    {
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string CountrySubDivisionCode { get; set; }
        public string PostalCode { get; set; }
    }
    public class ShipAddr
    {
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string CountrySubDivisionCode { get; set; }
        public string PostalCode { get; set; }
    }
}
