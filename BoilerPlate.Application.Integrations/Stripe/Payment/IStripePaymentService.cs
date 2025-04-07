using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerPlate.Application.Integrations.Stripe.Payment
{
    public interface IStripePaymentService
    {
        /// <summary>
        /// GeneratePaymentIntent
        /// </summary>
        /// <param name="amount">El monto por que se va a generar la inteno de pago</param>
        /// <returns></returns>
        Task<PaymentIntent> GeneratePaymentIntent(long amount);

        /// <summary>
        /// GetPaymentIntent
        /// </summary>
        /// <param name="paymentIntentId">El id del intento de pago</param>
        /// <returns></returns>
        Task<PaymentIntent> GetPaymentIntent(string paymentIntentId);

    }
}
