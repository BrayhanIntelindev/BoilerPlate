using BoilerPlate.Domain.Entities.Settings.Dynamics;

namespace BoilerPlate.Application.Integrations.Dynamics365.Authorization
{
    public interface IDynamicsAuthorizationService
    {

        #region BL

        MicrosoftDynamicsSettings Settings { get; set; }

        #endregion
        /// <summary>
        /// GetAccessTokenAsync
        /// </summary>
        /// <returns></returns>
        Task<string> GetAccessTokenAsync();
    }
}