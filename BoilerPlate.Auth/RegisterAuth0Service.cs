using BoilerPlate.Auth.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace BoilerPlate.Auth
{
    public static class RegisterAuth0Service
    {
        public static void AddAuth0Auth(this IServiceCollection services, IConfiguration configuration, bool multiple = false)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = $"https://{configuration["Auth0:Domain"]}/";
                    options.Audience = configuration["Auth0:Audience"];
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = ClaimTypes.NameIdentifier
                    };
                });
        }

        public static void AddAuth0Scopes(this IServiceCollection services, string[] scopes)
        {
            services.AddAuthorization(options =>
            {
                foreach (var scope in scopes)
                {
                    options.AddPolicy(scope, policy => policy.RequireClaim("scope", scope));
                }
            });
        }

        public static void AddAuth0Layer(this IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();
        }
    }
}
