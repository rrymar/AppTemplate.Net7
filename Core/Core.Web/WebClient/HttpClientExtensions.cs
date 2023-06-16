using System.Net.Http;
using System.Net.Http.Headers;

namespace Core.Web.WebClient
{
    public static class HttpClientExtensions
    {
        public static HttpClient AcceptJson(this HttpClient client)
        {
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

        public static void AddAuthorizationHeader(this IHttpClient client, string value)
        {
            client.RemoveHeader("Authorization");
            client.AddHeader("Authorization", value);
        }
    }
}
