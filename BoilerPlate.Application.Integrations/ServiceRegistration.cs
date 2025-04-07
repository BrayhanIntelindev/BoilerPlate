using BoilerPlate.Application.Integrations.Base;
using BoilerPlate.Application.Integrations.Dynamics365.Authorization;
using BoilerPlate.Application.Integrations.Dynamics365.Base;
using BoilerPlate.Application.Integrations.Dynamics365.Companies;
using BoilerPlate.Application.Integrations.Woocommerce.Authorization;
using BoilerPlate.Application.Integrations.Woocommerce.Product;
using Microsoft.Extensions.DependencyInjection;

namespace BoilerPlate.Application.Integrations
{
    public static class ServiceRegistration
    {
        public static void AddIntegrationLayer(this IServiceCollection services)
        {
            services.AddTransient<HttpContextMiddleware>();

            services.AddHttpClient<IntegrationServiceBase>()
                    .AddHttpMessageHandler<HttpContextMiddleware>();

            //CRM
            services.AddSingleton<ICRMAuthorizationService, CRMAuthorizationService>();
            services.AddTransient<ICRMCompanyService, CRMCompanyService>();

            //Woocommerce
            services.AddSingleton<IWoocommerceConnectService, WoocommerceConnectService>();
            services.AddTransient<IWoocommerceProductService, WoocommerceProductService>();
        }
    }
}
