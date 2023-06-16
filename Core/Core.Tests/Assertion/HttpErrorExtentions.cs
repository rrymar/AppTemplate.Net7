using Core.Web.WebClient;
using FluentAssertions;
using FluentAssertions.Specialized;
using System;
using System.Linq;
using System.Net;

namespace Core.Tests.Assertion
{
    public static class HttpErrorExtentions
    {
        public static ExceptionAssertions<ErrorResponseException> ShouldThrowValidationError<T>(this Func<T> action, params string[] messages)
        {
            return action.ShouldThrowHttpError((HttpStatusCode)422, messages);
        }

        public static ExceptionAssertions<ErrorResponseException> ShouldThrowHttpError<T>(this Func<T> action, string message)
        {
            var assertions = action.Should().Throw<ErrorResponseException>();
            var exception = assertions.And;
            VerifyMessages(exception, message);
            return assertions;
        }

        public static ExceptionAssertions<ErrorResponseException> ShouldThrowHttpError<T>(this Func<T> func, HttpStatusCode code, params string[] messages)
        {
            Action action = () => { func(); };

            return action.ShouldThrowHttpError(code, messages);
        }

        public static ExceptionAssertions<ErrorResponseException> ShouldThrowHttpError(this Action action, HttpStatusCode code, params string[] messages)
        {
            var assertions = action.Should().Throw<ErrorResponseException>();
            var exception = assertions.And;
            VerifyCode(exception, code);
            VerifyMessages(exception, messages);

            return assertions;
        }


        private static void VerifyMessages(ErrorResponseException exception, params string[] messages)
        {
            if (messages.Length > 0)
            {
                var errors = exception.ErrorResult?.Errors ?? new string[] { };
                messages.All(m => errors.Any(e => e.Contains(m))).Should().BeTrue();
            }
        }

        private static void VerifyCode(ErrorResponseException exception, HttpStatusCode code)
        {
            exception.StatusCode.Should().Be(code);
        }
    }
}
