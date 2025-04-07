using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerPlate.Application.Integrations.QuickBooks.Dto.Payment
{
    public class PaymentResponse
    {
        public Payment Payment { get; set; }
    }

    public class Payment
    {
        public long Id { get; set; }
        public string SyncToken { get; set; }
        public decimal TotalAmt { get; set; }
        public CustomerRefResponse CustomerRef { get; set; }
        public decimal UnappliedAmt { get; set; }
        public DepositToAccountRef DepositToAccountRef { get; set; }
        public string PrivateNote { get; set; }
    }
    public class CustomerRefResponse
    {
        public string Value { get; set; }
        public string Name { get; set; }
    }
    public class DepositToAccountRef
    {
        public string Value { get; set; }
    }
}
