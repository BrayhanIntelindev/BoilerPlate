using BoilerPlate.Localization.Resources;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System.Globalization;

namespace BoilerPlate.Localization
{
    public static class ServiceRegistration
    {
        public static void AddLocalizationLayer(this IServiceCollection services)
        {
            services.AddSingleton<GlobalizacionService>();
        }
    }
}
