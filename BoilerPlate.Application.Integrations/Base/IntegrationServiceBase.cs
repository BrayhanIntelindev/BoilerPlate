using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BoilerPlate.Application.Integrations.Base
{
    public class IntegrationServiceBase
    {
        HttpClient _apiClient;

        public IntegrationServiceBase() : base()
        {

        }

        public HttpClient ApiClient
        {
            get { return _apiClient; }
            set
            {
                _apiClient = value;
            }
        }

        public async Task<T?> GetAsync<T>(string uri)
        {
            return await _apiClient.GetFromJsonAsync<T>(uri);
        }

        public async Task<HttpResponseMessage> GetAsync<T>(string uri, T value)
        {
            return await _apiClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, uri) { Content = Serialize(value) });
        }

        public async Task<HttpResponseMessage> PostAsync<T>(string uri, T value)
        {
            return await _apiClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, uri) { Content = Serialize(value) });
        }

        public async Task<HttpResponseMessage> PostAsync(string uri)
        {
            return await _apiClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, uri));
        }

        public async Task<HttpResponseMessage> PutAsync<T>(string uri, T value)
        {

            return await _apiClient.SendAsync(new HttpRequestMessage(HttpMethod.Put, uri) { Content = Serialize(value) });
        }

        public async Task<HttpResponseMessage> DeleteAsync<T>(string uri, T value)
        {
            return await _apiClient.SendAsync(new HttpRequestMessage(HttpMethod.Delete, uri) { Content = Serialize(value) });
        }

        public async Task<HttpResponseMessage> DeleteAsync(string uri)
        {
            return await _apiClient.SendAsync(new HttpRequestMessage(HttpMethod.Delete, uri));
        }

        //Comentado ya que en QB el enviar las propiedades con valores null, estaba generando problemas.
        //private static HttpContent Serialize(object data) => new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

        private static HttpContent Serialize(object data) => new StringContent(JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.None,
                                   new JsonSerializerSettings
                                   {
                                       NullValueHandling = NullValueHandling.Ignore
                                   }), Encoding.UTF8, "application/json");
    }
}
