namespace BoilerPlate.Application.Integrations.QuickBooks.Dto.Customer
{
    public class CustomerRequest
    {
        public string DisplayName { get; set; }
        public string CompanyName { get; set; }
        public bool Taxable { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string Notes { get; set; }
        public BillAddr BillAddr { get; set; }
        public PrimaryPhone PrimaryPhone { get; set; }
        public PrimaryEmailAddr PrimaryEmailAddr { get; set; }
        public string Id { get; set; }
        public string SyncToken { get; set; }
        /// <summary>
        /// OJO: Se debe marcar null cuando es Taxable = true, de tal manera que luego se pueda eliminar el atributo en el Json que va al api
        /// </summary>
        public int? TaxExemptionReasonId { get; set; }
        /// <summary>
        /// OJO: Se debe marcar null cuando es Taxable = true, de tal manera que luego se pueda eliminar el atributo en el Json que va al api
        /// </summary>
        public string ResaleNum { get; set; }

    }

    public class BillAddr
    {
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string CountrySubDivisionCode { get; set; }
        public string PostalCode { get; set; }
        public string Id { get; set; }
    }

    public class PrimaryPhone
    {
        public string FreeFormNumber { get; set; }
    }

    public class PrimaryEmailAddr
    {
        public string Address { get; set; }
    }
}
