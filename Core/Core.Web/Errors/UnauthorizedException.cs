using System;
using System.Net;
using System.Runtime.Serialization;

namespace Core.Web.Errors
{
    public class UnauthorizedException : HttpStatusException
    {
        public override HttpStatusCode Status => HttpStatusCode.Unauthorized;

        public UnauthorizedException()
        {
        }

        public UnauthorizedException(string message) : base(message)
        {
        }

        public UnauthorizedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected UnauthorizedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
