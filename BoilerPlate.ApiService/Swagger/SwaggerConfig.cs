using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Stripe;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BoilerPlate.ApiService.Swagger
{
    public static class SwaggerConfig
    {
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                // Documento Swagger para WooCommerce
                options.SwaggerDoc("woocommerce", new OpenApiInfo { Title = "WooCommerce API", Version = "v1" });

                options.TagActionsBy(api => new[] { api.GroupName });
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });
            
        }

        public static void UseConfiguredSwagger(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/woocommerce/swagger.json", "WooCommerce API");

                // Genera endpoints de Swagger para cada versión descubierta de la API
                //foreach (var description in provider.ApiVersionDescriptions)
                //{
                //    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToLowerInvariant());
                //}
            });


        }
        public static void MapDefaultSwagger(this IEndpointRouteBuilder endpoints, IApiVersionDescriptionProvider provider, string defaultVersion)
        {
            endpoints.MapGet("/swagger", context =>
            {
                context.Response.Redirect($"/swagger/{defaultVersion}/index.html");
                return Task.CompletedTask;
            });
        }

    }
}
