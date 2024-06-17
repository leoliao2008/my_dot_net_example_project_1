using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace MinimalApiTutorial.ErrorHandler
{
    public class GlobleExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobleExceptionHandler> _logger;

        public GlobleExceptionHandler(ILogger<GlobleExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception,@"Exception:{Message}",exception.Message);
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            httpContext.Response.ContentType = "application/json";
            ProblemDetails problemDetails = new()
            {
                Status = httpContext.Response.StatusCode,
                Title = "Internal Server Error",
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1"
            };
            await httpContext.Response.WriteAsJsonAsync(problemDetails,cancellationToken);
            return true;
        }
    }
}
