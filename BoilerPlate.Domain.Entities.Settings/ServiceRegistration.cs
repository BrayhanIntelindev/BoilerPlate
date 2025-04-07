using BoilerPlate.Domain.Entities.Settings.Base;
using BoilerPlate.Domain.Entities.Settings.Dynamics;
using BoilerPlate.Domain.Entities.Settings.Notifications;
using BoilerPlate.Domain.Entities.Settings.QuickBook;
using BoilerPlate.Domain.Entities.Settings.Woocommerce;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace BoilerPlate.Domain.Entities.Settings
{
    public static class ServiceRegistration
    {
        public static void ConfigureAppSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var settingsTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(AppSettingsBase)));

            foreach (var type in settingsTypes)
            {
                Configure(type, services, configuration);
            }
        }

        private static void Configure(Type type, IServiceCollection services, IConfiguration configuration)
        {
            var section = configuration.GetSection(type.Name);
            if (section.Exists())
            {
                var method = typeof(OptionsConfigurationServiceCollectionExtensions)
                    .GetMethods(BindingFlags.Static | BindingFlags.Public)
                    .First(m => m.Name == nameof(OptionsConfigurationServiceCollectionExtensions.Configure) && m.GetParameters().Length == 2)
                    .MakeGenericMethod(type);

                method.Invoke(null, [services, section]);
            }
        }
    }
}
