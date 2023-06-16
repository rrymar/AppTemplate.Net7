using Core.Web.WebClient;

namespace Core.Tests
{
    public class TestingHttpClient : DefaultHttpClient
    {
        public TestingHttpClient(System.Net.Http.HttpClient httpClient) : base(httpClient)
        {
        }
    }
}
