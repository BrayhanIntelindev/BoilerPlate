using BoilerPlate.Application.Integrations.Stripe.Authorization;
using Stripe;

namespace BoilerPlate.Application.Integrations.Stripe.Payment
{
    public class StripePaymentService : IStripePaymentService
    {
        private readonly IStripeConnectService _stripeConnectService;
        public StripePaymentService(IStripeConnectService stripeConnectService)
        {
            _stripeConnectService = stripeConnectService;
        }

        public async Task<PaymentIntent> GeneratePaymentIntent(long amount)
        {
            StripeConfiguration.ApiKey = _stripeConnectService.GenerateToken();

            var paymentIntentService = new PaymentIntentService();
            var paymentIntent = await paymentIntentService.CreateAsync(new PaymentIntentCreateOptions
            {
                Amount = amount,
                Currency = "usd",
                PaymentMethodTypes = ["us_bank_account", "card"],
            });

            return paymentIntent;
        }

        public async Task<PaymentIntent> GetPaymentIntent(string paymentIntentId)
        {
            StripeConfiguration.ApiKey = _stripeConnectService.GenerateToken();

            var service = new PaymentIntentService();

            return await service.GetAsync(paymentIntentId);
        }
    }
}
