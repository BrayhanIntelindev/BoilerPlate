using BoilerPlate.Application.Sections.Exceptions;
using Intuit.Ipp.Core.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace BoilerPlate.Application.Base
{
    public class BaseService(GlobalizacionService globalizacionService, ILogger logger) : object() 
    {
        IHttpContextAccessor _httpContextAccessor;
        protected readonly GlobalizacionService _globalizacionService = globalizacionService;
        protected readonly ILogger _logger = logger;

        public IHttpContextAccessor HttpContextAccessor
        {
            get { return _httpContextAccessor; }
            set
            {
                _httpContextAccessor = value;
            }
        }

        public string CurrentDomain => string.Format("{0}://{1}/", _httpContextAccessor.HttpContext.Request.Scheme, _httpContextAccessor.HttpContext.Request.Host.Value);

        public string AhtLogoPath => string.Format("{0}://{1}/{2}", _httpContextAccessor.HttpContext.Request.Scheme, _httpContextAccessor.HttpContext.Request.Host.Value, "public/images/logos/ahtwindowsllc-default-logo.png");

        public string CurrentDomainRequest()
        {
            var domain = _httpContextAccessor.HttpContext.Request.Headers["Origin"].FirstOrDefault() ?? CurrentDomain;
            return $"{domain}/#/";
        }

        /// <summary>
        /// Error with generic message
        /// </summary>
        /// <exception cref="ApiException"></exception>
        public void StopAndSendApiException()
        {
            throw new ApiException(_globalizacionService.GetErrorMessage().OperationNotExecuted);
        }

        /// <summary>
        /// Error with message
        /// </summary>
        /// <param name="message"></param>
        public static void StopAndSendApiException(string message) => throw new ApiException($"{message}");


        /// <summary>
        /// Error with code and message
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="message"></param>
        /// <exception cref="ApiException"></exception>
        public static void StopAndSendApiException(HttpStatusCode errorCode, string message) => throw new ApiException(errorCode, $"{message}");


        /// <summary>
        /// Default failed response
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static void FailedResponse(string message) => throw new ApiException(HttpStatusCode.BadRequest, $"{message}");


        /// <summary>
        /// SetCookie
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="cookieOptions"></param>
        public void SetCookie(string key, string value, CookieOptions cookieOptions)
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Append(key, value, cookieOptions);
        }

        /// <summary>
        /// GetCookie
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetCookie(string key)
        {
            return _httpContextAccessor.HttpContext.Request.Cookies[key];
        }

        /// <summary>
        /// RemoveCookie
        /// </summary>
        /// <param name="key"></param>
        public void RemoveCookie(string key)
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Delete(key);
        }

        protected DateTime ToClientTimeZone(DateTime utcDate)
        {
            return utcDate.ToClientTimeZone(_httpContextAccessor.HttpContext.Request.Headers);
        }

        protected Response<TAppResponse> TransformToAppResponse<TIntegrationResponse, TAppResponse>(
        TIntegrationResponse? integrationResponse,
        Func<TIntegrationResponse, TAppResponse> toDto)
        {
            if (integrationResponse == null)
            {
                StopAndSendApiException(HttpStatusCode.NotFound, _globalizacionService.GetErrorMessage().NotFound);
                return new Response<TAppResponse>();
            }
            else
            {
                return new Response<TAppResponse>(toDto(integrationResponse));
            }
        }

        protected Response<List<TAppResponse>> TransformToAppResponseList<TIntegrationResponse, TAppResponse>(
            List<TIntegrationResponse> integrationResponseList,
            Func<TIntegrationResponse, TAppResponse> toDto)
        {
            var appResponseList = integrationResponseList.Select(toDto).ToList();
            return new Response<List<TAppResponse>>(appResponseList);
        }

        ///// <summary>
        ///// UserSessionAsync
        ///// </summary>
        ///// <returns></returns>
        //public async Task UserSessionAsync()
        //{
        //    //todo: implement logic
        //    await Task.CompletedTask;
        //    return;
        //}


        ///// <summary>
        ///// Validate if user in session is admin
        ///// </summary>
        ///// <returns></returns>
        //public bool IsAdminUserAsync()
        //{
        //    //todo: implement logic
        //    return true;
        //}
    }
}
