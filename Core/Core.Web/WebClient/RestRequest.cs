using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Core.Web.WebClient
{
    public class RestRequest
    {
        public string Url { get; private set; }

        public IDictionary<string, string> Headers { get; } = new Dictionary<string, string>();

        public HttpContent Content { get; private set; }

        public RestRequest(string url)
        {
            Url = url;
        }

        public void AddUrlSegment(string segment)
        {
            Url = $"{Url}/{segment}";
        }

        public void ReplaceUrlSegment(string segment, object value)
        {
            Url = Url.Replace($"{{{segment}}}", value.ToString());
        }

        public void AddUrlSegment(int segment)
        {
            AddUrlSegment(segment.ToString());
        }

        public void AddQueryParam(string name, string value)
        {
            var separator = Url.Contains('?') ? "&" : "?";
            Url += $"{separator}{name}={value}";
        }

        public void AddQueryToUrl<TQuery>(TQuery query)
            where TQuery : class
        {
            var queryString = string.Empty;
            if (query != null)
            {
                var prefix = "?";
                var type = typeof(TQuery);
                foreach (var prop in type.GetProperties())
                {
                    if (prop.PropertyType == typeof(DateTime?))
                    {
                        var propValue = prop.GetValue(query);
                        if (propValue != null)
                        {
                            var value = Convert.ToDateTime(propValue).ToString("yyyy-MM-ddTHH:mm:ss");
                            queryString += $"{prefix}{prop.Name}={value}";
                        }
                    }
                    else
                    {
                        queryString += $"{prefix}{prop.Name}={prop.GetValue(query)}";
                    }
                    prefix = "&";
                }
            }

            Url += queryString;
        }

        public void AddJsonContent(object obj)
        {
            Content = new StringContent(obj.ToJson());
            Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        }

        public void AddFileContent(byte[] file, string fileName, string mediaType = "application/octet-stream")
        {
            var fileContent = new ByteArrayContent(file);
            AddFileContentImpl(fileName, mediaType, fileContent);
        }

        public void AddFileContent(Stream file, string fileName, string mediaType)
        {
            var fileContent = new StreamContent(file);
            AddFileContentImpl(fileName, mediaType, fileContent);
        }

        private void AddFileContentImpl(string fileName, string mediaType, HttpContent fileContent)
        {
            var requestContent = new MultipartFormDataContent();
            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse(mediaType);
            requestContent.Add(fileContent, "file", fileName);

            Content = requestContent;
        }

        public void VisitClient(IHttpClient client)
        {
            foreach (var item in Headers)
            {
                client.AddHeader(item.Key, item.Value);
            }
        }
    }

    public static class RestRequestExtensions
    {
        public static RestRequest ToRestRequest(this string url)
        {
            return new RestRequest(url);
        }
    }
}
