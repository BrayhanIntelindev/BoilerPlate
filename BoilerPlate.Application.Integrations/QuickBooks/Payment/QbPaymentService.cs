using BoilerPlate.Application.Integrations.QuickBooks.Authorization;
using BoilerPlate.Application.Integrations.QuickBooks.Base;
using BoilerPlate.Application.Integrations.QuickBooks.Dto.Payment;
using Intuit.Ipp.Core.Configuration;

namespace BoilerPlate.Application.Integrations.QuickBooks.Payment
{
    internal class QbPaymentService(HttpClient apiClient, IQuickBooksConnectService quickBooksConnectService) : QbServiceBase<PaymentRequest, PaymentResponse>(apiClient, quickBooksConnectService, "invoice")
    {
        new public async Task<PaymentResponse> UpdateAsync(PaymentRequest dto)
        {
            // Payment not updateds in QuickBooks
            throw new NotImplementedException();
        }
    }
}
