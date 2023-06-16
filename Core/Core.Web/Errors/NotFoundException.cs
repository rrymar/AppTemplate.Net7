using System;
using System.Net;
using System.Runtime.Serialization;

namespace Core.Web.Errors
{
    public class NotFoundException : HttpStatusException
    {
        public override HttpStatusCode Status => HttpStatusCode.NotFound;

        public NotFoundException()
        {
        }

        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected NotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}