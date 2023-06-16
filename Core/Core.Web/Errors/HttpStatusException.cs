using System;
using System.Net;
using System.Runtime.Serialization;

namespace Core.Web.Errors
{
    public abstract class HttpStatusException : Exception
    {
        public abstract HttpStatusCode Status { get; }

        protected HttpStatusException()
        {
        }

        protected HttpStatusException(string message) : base(message)
        {
        }

        protected HttpStatusException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected HttpStatusException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}