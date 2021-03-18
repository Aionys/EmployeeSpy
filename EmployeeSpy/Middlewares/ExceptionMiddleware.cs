using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace EmployeeSpy.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;


        public ExceptionMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<ExceptionMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleException(context.Response, ex, HttpStatusCode.InternalServerError);
            }
        }

        private Task HandleException(HttpResponse response, Exception exception, HttpStatusCode statusCode)
        {
            _logger.Log(LogLevel.Error, exception, exception.Message);
            response.StatusCode = (int)statusCode;
            return response.WriteAsync($"Error '{exception.Message}' occured. See logs for details.");
        }
    }
}
