using System.Globalization;
using System.Net;

namespace BoilerPlate.Application.Sections.Exceptions
{
    public class ApiException : Exception
    {
        public HttpStatusCode ErrorCode { get; private set; } = HttpStatusCode.InternalServerError;

        public ApiException() : base() { }

        public ApiException(string message) : base(message) { }

        public ApiException(HttpStatusCode errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }

        public ApiException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {

        }
    }
}
