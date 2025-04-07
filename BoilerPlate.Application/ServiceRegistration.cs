using BoilerPlate.Application.Woocommerce.Product;
using Microsoft.Extensions.DependencyInjection;

namespace BoilerPlate.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddTransient<IProductService, ProductService>();
        }
    }
}
