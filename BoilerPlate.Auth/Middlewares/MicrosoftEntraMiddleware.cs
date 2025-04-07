using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Text;

namespace BoilerPlate.Auth.Middlewares
{
    public class MicrosoftEntraMiddleware(RequestDelegate next, string secretKey)
    {
        private readonly RequestDelegate _next = next;
        private readonly string _secretKey = secretKey;

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                try
                {
                    await AttachUserToContext(context, token);
                }
                catch (AuthenticationException)
                {
                    context.Response.StatusCode = 401; // Unauthorized
                    return;
                }
            }

            await _next(context);
        }

        private async Task AttachUserToContext(HttpContext context, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_secretKey);

                var validationResult = await tokenHandler.ValidateTokenAsync(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // if you want to check the token expiration, set this to true
                    ValidateLifetime = false,
                    // Allows for a small amount of clock drift.
                    ClockSkew = TimeSpan.Zero
                });

                SecurityToken validatedToken = validationResult.SecurityToken;
                var jwtToken = (JwtSecurityToken)validatedToken;
                // Attach user to context on successful jwt validation
                if(jwtToken == null) 
                    //aqui debe tirar la excepcion de unauutorize
                context.Items["UserId"] = jwtToken.Claims.First(x => x.Type == "id").Value;
            }
            catch (SecurityTokenValidationException ex)
            {
                // Throw an Unauthorized exception for validation failures
                Console.WriteLine($"Error de validación del token: {ex.Message}");
                throw new AuthenticationException("Token validation failed: Unauthorized");
            }
            catch (Exception ex)
            {
                // Handle other exceptions (if necessary)
                Console.WriteLine($"Unexpected error during token validation: {ex.Message}");
                throw new AuthenticationException("Unexpected error during token validation");
            }
        }
    }
}
