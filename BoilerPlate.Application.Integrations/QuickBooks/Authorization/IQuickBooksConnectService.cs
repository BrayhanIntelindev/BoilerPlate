
using BoilerPlate.Domain.Entities.Settings.QuickBook;

namespace BoilerPlate.Application.Integrations.QuickBooks.Authorization
{
    public interface IQuickBooksConnectService
    {
        #region BL

        QuickBooksSetting QuickBooksSetting { get; set; }

        #endregion

        /// <summary>
        /// AuthenticateUserAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<object> AuthenticateUserAsync();

        /// <summary>
        /// Get a new access token from refresh token.
        /// </summary>
        /// <returns></returns>
        Task<string> GetAccessTokenAsync();
    }
}
