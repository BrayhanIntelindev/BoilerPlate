using BoilerPlate.Application.Integrations.QuickBooks.Authorization;
using BoilerPlate.Application.Integrations.QuickBooks.Base;
using BoilerPlate.Application.Integrations.QuickBooks.Dto.Invoice;
using Newtonsoft.Json;


namespace BoilerPlate.Application.Integrations.QuickBooks.Invoice
{
    public class QbInvoiceService(HttpClient apiClient, IQuickBooksConnectService quickBooksConnectService) : QbServiceBase<InvoiceRequest, InvoiceResponse>(apiClient, quickBooksConnectService, "invoice")
    {
        public async Task<InvoiceResponse> SendAsync(long id)
        {
            try
            {
                await SetAuthHeader();

                var result = await PostAsync(string.Format("{0}/{1}/send", _endpoint, id));

                return JsonConvert.DeserializeObject<InvoiceResponse>(await result.Content.ReadAsStringAsync());
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
