using BoilerPlate.Application.Integrations.QuickBooks.Base;
using BoilerPlate.Application.Integrations.QuickBooks.Dto.Invoice;

namespace BoilerPlate.Application.Integrations.QuickBooks.Invoice
{
    public interface IQbInvoiceService : IQbServiceBase<InvoiceRequest, InvoiceResponse>
    {
        /// <summary>
        /// Send an invoice
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<InvoiceResponse> SendAsync(long id);
    }
}
