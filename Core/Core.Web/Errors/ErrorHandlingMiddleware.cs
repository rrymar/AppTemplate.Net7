using Core.Web.WebClient;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Web.Errors
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;

        private readonly ILogger logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            this.next = next;
            logger = loggerFactory.CreateLogger("ErrorHandling");
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (DbUpdateConcurrencyException e)
            {
                context.Response.Headers.Add("Location", context.Request.Path.Value);
                HandleException(context, e, 307, e.Message);
            }
            catch (HttpStatusException e)
            {
                HandleException(context, e, (int)e.Status, e.Message);
            }
            catch (UnauthorizedAccessException e)
            {
                HandleException(context, e, 403, e.Message);
            }
            catch (AggregateException e)
            {
                HandleException(context, e, 500, e.InnerExceptions.Select(i => i.Message).ToArray());
            }
            catch (Exception e)
            {
                var innerUnauthorized = GetUnauthorizedAccessException(e);
                if (innerUnauthorized != null)
                {
                    HandleException(context, e, 403, e.Message);
                    return;
                }
                HandleException(context, e, 500, e.Message);
            }
        }

        private UnauthorizedAccessException GetUnauthorizedAccessException(Exception e)
        {
            if (e.InnerException is UnauthorizedAccessException innerUnauthorized)
                return innerUnauthorized;

            innerUnauthorized = e.InnerException?.InnerException as UnauthorizedAccessException;

            return innerUnauthorized ?? e.InnerException?.InnerException?.InnerException as UnauthorizedAccessException;
        }

        private void HandleException(HttpContext context, Exception e, int code, params string[] errors)
        {
            logger.LogError(e, "Unhandled exception");
            if (!context.Response.HasStarted)
            {
                var responseHeaders = new HeaderDictionary();
                foreach (var header in context.Response.Headers)
                    responseHeaders.Add(header);

                context.Response.Clear();

                foreach (var header in responseHeaders)
                    context.Response.Headers.Add(header);
            }

            WriteErrorResponse(context, code, errors);
        }

        private static void WriteErrorResponse(HttpContext context, int code, IEnumerable<string> errors)
        {
            var response = new ErrorResult(errors, context.TraceIdentifier);
            context.Response.StatusCode = code;
            context.Response.ContentType = "application/json";
            context.Response.WriteAsync(response.ToJson());
        }
    }
}
