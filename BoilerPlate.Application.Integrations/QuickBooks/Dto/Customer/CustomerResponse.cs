namespace BoilerPlate.Application.Integrations.QuickBooks.Dto.Customer
{
    public class CustomerResponse
    {
        public Customer Customer { get; set; }
    }

    public class Customer
    {
        public long Id { get; set; }
        public string SyncToken { get; set; }
        public string CompanyName { get; set; }
        public BillAddrResponse BillAddr { get; set; }
    }
    public class BillAddrResponse
    {
        public long Id { get; set; }

    }
}
