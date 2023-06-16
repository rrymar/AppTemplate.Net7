using System;
using System.Net;

namespace Core.Web.WebClient
{
    public class ErrorResponseException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }

        public ErrorResult ErrorResult { get; }

        public override string Message => $"Http status: {StatusCode}; {ErrorResult}";

        public ErrorResponseException(ErrorResult errorResult, HttpStatusCode statusCode)
        {
            ErrorResult = errorResult;
            StatusCode = statusCode;
        }

        public ErrorResponseException() : base()
        {
        }

        protected ErrorResponseException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }

        public ErrorResponseException(string message) : base(message)
        {
        }

        public ErrorResponseException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
