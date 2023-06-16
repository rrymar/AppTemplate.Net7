using System;
using System.Net;
using System.Runtime.Serialization;

namespace Core.Web.Errors
{
    public class BusinessValidationException : HttpStatusException
    {
        public override HttpStatusCode Status => (HttpStatusCode)422;

        public BusinessValidationException()
        {
        }

        public BusinessValidationException(string message) : base(message)
        {
        }

        public BusinessValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected BusinessValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}