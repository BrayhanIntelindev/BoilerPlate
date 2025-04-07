using BoilerPlate.Domain.Entities.Settings.Dynamics;

namespace BoilerPlate.Application.Integrations.Dynamics365.Authorization
{
    public interface ICRMAuthorizationService
    {

        #region BL

        CRMDynamicsSettings Settings { get; set; }

        #endregion
        /// <summary>
        /// GetAccessTokenAsync
        /// </summary>
        /// <returns></returns>
        Task<string> GetAccessTokenAsync();
    }
}