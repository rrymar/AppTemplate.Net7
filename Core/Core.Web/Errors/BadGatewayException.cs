using System;
using System.Net;
using System.Runtime.Serialization;

namespace Core.Web.Errors
{
    public class BadGatewayException : HttpStatusException
    {
        public override HttpStatusCode Status => HttpStatusCode.BadGateway;

        public BadGatewayException()
        {
        }

        public BadGatewayException(string message) : base(message)
        {
        }

        public BadGatewayException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected BadGatewayException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
