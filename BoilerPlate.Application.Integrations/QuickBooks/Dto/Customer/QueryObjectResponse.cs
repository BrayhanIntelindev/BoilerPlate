namespace BoilerPlate.Application.Integrations.QuickBooks.Dto.Customer
{
    public class QueryObjectResponse
    {
        public QueryResponse QueryResponse { get; set; }
    }

    public class QueryResponse
    {
        public List<Customer> Customer { get; set; }
    }
}
