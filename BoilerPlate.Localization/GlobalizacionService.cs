using BoilerPlate.Localization.Resources;
using Microsoft.Extensions.Configuration;
using System.Globalization;

namespace BoilerPlate.Localization
{
    public class GlobalizacionService
    {
        private readonly IConfiguration _configuration;
        private CultureInfo _currentCulture;

        public GlobalizacionService(IConfiguration configuration)
        {
            _configuration = configuration;
            SetDefaultCulture();
        }

        private void SetDefaultCulture()
        {
            var defaultLanguage = _configuration["DefaultLanguage"];
            _currentCulture = new CultureInfo(defaultLanguage ?? "en");
            CultureInfo.CurrentCulture = _currentCulture;
            CultureInfo.CurrentUICulture = _currentCulture;
        }

        public void SetCulture(string cultureName)
        {
            _currentCulture = new CultureInfo(cultureName);
            CultureInfo.CurrentCulture = _currentCulture;
            CultureInfo.CurrentUICulture = _currentCulture;
        }

        public CultureInfo GetCurrentCulture()
        {
            return _currentCulture;
        }

        public ErrorMessage GetErrorMessage()
        {
            return new ErrorMessage(_currentCulture.Name);
        }
    }
}
