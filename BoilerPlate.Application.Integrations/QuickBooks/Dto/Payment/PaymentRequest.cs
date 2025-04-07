using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerPlate.Application.Integrations.QuickBooks.Dto.Payment
{
    public class PaymentRequest
    {
        public decimal TotalAmt { get; set; }
        public CustomerRefPayment CustomerRef { get; set; }
        public string PaymentRefNum { get; set; }
        public string PrivateNote { get; set; }
        public List<Line> Line { get; set; }

    }

    public class CustomerRefPayment
    {
        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public class Line
    {
        public decimal Amount { get; set; }
        public List<LinkedTxn> LinkedTxn { get; set; }
    }

    public class LinkedTxn
    {
        public string TxnId { get; set; }
        public string TxnType { get; set; }
    }
}
