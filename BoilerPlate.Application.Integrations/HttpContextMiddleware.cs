using Microsoft.Extensions.DependencyInjection;

namespace BoilerPlate.Application.Integrations
{
    public class HttpContextMiddleware : DelegatingHandler
    {
        public HttpContextMiddleware()
        {

        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return base.SendAsync(request, cancellationToken);
        }
    }
}
