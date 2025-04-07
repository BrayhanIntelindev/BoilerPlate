using BoilerPlate.Auth.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;

namespace BoilerPlate.Auth
{
    public static class RegisterAuthService
    {
        public static void AddMicrosoftEntraAuth(this IServiceCollection services, IConfiguration configuration, bool multiple = false)
        {
            if (multiple)
                // If you have multiple Azure AD applications, you can configure them here.
                services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(configuration.GetSection("MultipleAzureAd"), jwtBearerScheme: "AzureAdMultiple");
            else
                // Add authentication services
                services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(configuration.GetSection("AzureAd"));
        }

        public static IApplicationBuilder UseMicrosoftEntraValidation(this IApplicationBuilder builder, IConfiguration configuration)
        {
            string secretKey = configuration.GetValue<string>("Jwt:SecretKey");
            return builder.UseMiddleware<MicrosoftEntraMiddleware>(secretKey);
        }
    }
}
