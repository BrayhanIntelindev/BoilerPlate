using Microsoft.Extensions.Localization;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace BoilerPlate.Localization.Resources
{
    public class ErrorMessage(string cultureName)
    {
        private readonly ResourceManager _resourceManager = new("BoilerPlate.Localization.Resources.ErrorMessage", typeof(ErrorMessage).Assembly);
        private readonly CultureInfo _cultureInfo = new(cultureName);

        public string GetMessage(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return string.Empty;
            }

            if (_resourceManager == null)
            {
                return string.Empty;
            }

            var message = _resourceManager.GetString(key, _cultureInfo);
            return message ?? string.Empty;
        }
        public string GetMessage(string key, params object[] args)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return string.Empty;
            }

            var message = _resourceManager.GetString(key, _cultureInfo);
            return message == null ? string.Empty : string.Format(message, args);
        }
        public string OperationNotExecuted => GetMessage(nameof(OperationNotExecuted));
        public string BadRequest => GetMessage(nameof(BadRequest));
        public string NotFound => GetMessage(nameof(NotFound));
        public string RequestError => GetMessage(nameof(RequestError));
    }
}
