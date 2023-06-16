using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Core.Web.WebClient
{
    public static class HttpContentExtensions
    {
        private static readonly JsonSerializerOptions Options = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public static async Task<T> ReadAsAsync<T>(this HttpContent content)
        {
            return await JsonSerializer.DeserializeAsync<T>(
                await content.ReadAsStreamAsync(), Options);
        }
    }
}
