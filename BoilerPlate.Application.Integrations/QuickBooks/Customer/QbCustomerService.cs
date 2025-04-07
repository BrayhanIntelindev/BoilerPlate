using BoilerPlate.Application.Integrations.QuickBooks.Authorization;
using BoilerPlate.Application.Integrations.QuickBooks.Base;
using BoilerPlate.Application.Integrations.QuickBooks.Dto.Customer;
namespace BoilerPlate.Application.Integrations.QuickBooks.Customer
{
    public class QbCustomerService(HttpClient apiClient, IQuickBooksConnectService quickBooksConnectService) : 
        QbServiceBase<CustomerRequest, CustomerResponse>(apiClient, quickBooksConnectService, "customer"), IQbCustomerService
    {
        public async Task<QueryObjectResponse> GetByCustomerNameAsync(string name)
        {
            try
            {
                await SetAuthHeader();
                return await GetAsync<QueryObjectResponse>(string.Format("query?query=select * from Customer Where CompanyName LIKE '%{0}%'&minorversion={1}", name, _quickBooksConnectService.QuickBooksSetting.MinorVersion));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}