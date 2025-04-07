
using Microsoft.Extensions.Options;
using Intuit.Ipp.OAuth2PlatformClient;
using BoilerPlate.Domain.Entities.Settings.QuickBook;

namespace BoilerPlate.Application.Integrations.QuickBooks.Authorization
{
    public class QuickBooksConnectService : IQuickBooksConnectService
    {
        public QuickBooksSetting QuickBooksSetting { get; set; }
        public OAuth2Client oAuth2Client;

        public QuickBooksConnectService(IOptions<QuickBooksSetting> quickBooksSetting)
        {
            QuickBooksSetting = quickBooksSetting.Value;
            oAuth2Client = new OAuth2Client(QuickBooksSetting.ClientId, QuickBooksSetting.ClientSecret, QuickBooksSetting.RedirectUrl, QuickBooksSetting.Environment);
        }

        public async Task<object> AuthenticateUserAsync()
        {
            // TODO: Implement this method
            //var tokenResponse = await oAuth2Client.GetBearerTokenAsync("AB11671743371w5gxD6LVpxkKdgarIcYBGuJ1puN6OcYEhPUR6");

            //return new
            //{
            //    AccessToken = tokenResponse.AccessToken,
            //    RefreshToken = tokenResponse.RefreshToken
            //};

            return new { };
        }

        public async Task<string> GetAccessTokenAsync()
        {
            // TODO: Implement this method
            //var authToken = await _quickBooksAuthRepository.GetLastRegisterFromAhtAsync();

            //if (authToken.AccessExpirationDate > DateTime.UtcNow)
            //    return authToken.AccessToken;

            //var response = await oAuth2Client.RefreshTokenAsync(authToken.RefreshToken);

            //if (response.HttpStatusCode == HttpStatusCode.OK)
            //{
            //    authToken.AccessToken = response.AccessToken;
            //    authToken.AccessExpirationDate = DateTime.UtcNow.AddSeconds(response.AccessTokenExpiresIn);

            //    if (authToken.RefreshToken != response.RefreshToken)
            //    {
            //        authToken.RefreshToken = response.RefreshToken;
            //        authToken.RefreshExpirationDate = DateTime.UtcNow.AddSeconds(response.RefreshTokenExpiresIn);
            //    }

            //    await _quickBooksAuthRepository.UpdateAsync(authToken);
            //}

            //return authToken.AccessToken;
            return string.Empty;
        }
    }

}
