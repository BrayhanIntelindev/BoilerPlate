using BoilerPlate.Application.Integrations.QuickBooks.Base;
using BoilerPlate.Application.Integrations.QuickBooks.Dto.Customer;

namespace BoilerPlate.Application.Integrations.QuickBooks.Customer
{
    public interface IQbCustomerService : IQbServiceBase<CustomerRequest, CustomerResponse>
    {
        /// <summary>
        /// Get customer by companyname
        /// </summary>
        /// <param name="companyName"></param>
        /// <returns></returns>
        Task<QueryObjectResponse> GetByCustomerNameAsync(string companyName);
    }
}
