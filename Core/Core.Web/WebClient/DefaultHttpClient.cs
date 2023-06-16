using System.Net.Http;
using System.Threading.Tasks;

namespace Core.Web.WebClient
{
    public class DefaultHttpClient : IHttpClient
    {
        private readonly HttpClient httpClient;

        public DefaultHttpClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public void AddHeader(string name, string value)
        {
            httpClient.DefaultRequestHeaders.Remove(name);
            httpClient.DefaultRequestHeaders.Add(name, value);
        }

        public Task<HttpResponseMessage> DeleteAsync(string requestUri)
        {
            return httpClient.DeleteAsync(requestUri);
        }

        public void Dispose()
        {
            httpClient.Dispose();
        }

        public Task<HttpResponseMessage> GetAsync(string requestUri)
        {
            return httpClient.GetAsync(requestUri);
        }

        public Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content)
        {
            return httpClient.PostAsync(requestUri, content);
        }

        public Task<HttpResponseMessage> PutAsync(string requestUri, HttpContent content)
        {
            return httpClient.PutAsync(requestUri, content);
        }

        public void RemoveHeader(string name)
        {
            httpClient.DefaultRequestHeaders.Remove(name);
        }

        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            return httpClient.SendAsync(request);
        }
    }
}
