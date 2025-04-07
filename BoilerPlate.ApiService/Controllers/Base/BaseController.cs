using BoilerPlate.Application.Sections.Exceptions;
using BoilerPlate.Localization;
using BoilerPlate.Localization.Resources;
using BoilerPlateProject.Application.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BoilerPlate.ApiService.Controllers.Base
{
    /// <summary>
    /// BaseController
    /// </summary>
    /// <remarks>
    /// BaseController
    /// </remarks>
    [ProducesResponseType(typeof(Response<object>), ((int)HttpStatusCode.BadRequest))]
    [ProducesResponseType(typeof(Response<object>), ((int)HttpStatusCode.InternalServerError))]
    public class BaseController : ControllerBase
    {
        public readonly GlobalizacionService _globalizacionService;

        public BaseController(GlobalizacionService globalizacionService)
        {
            _globalizacionService = globalizacionService;
        }

        /// <summary>
        /// Execute service
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serviceMethod"></param>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        protected async Task<IActionResult> ExecuteServiceAsync<T>(Func<Task<T>> serviceMethod)
        {
            try
            {
                var result = await serviceMethod();
                return Ok(result);
            }
            catch (ApiException ex)
            {
                return ProcessBadResponse(ex, _globalizacionService.GetErrorMessage().RequestError);
            }
        }

        /// <summary>
        /// Generic Base Response
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult ProcessBadResponse(ApiException ex, string message)
        {
            return new ObjectResult(new Response<string>(message,
                [
                    new ModelError(ex,ex.Message)
                ]))
            {
                StatusCode = (int)ex.ErrorCode
            };
        }
    }
}
