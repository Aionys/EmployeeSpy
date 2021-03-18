using EmployeeSpy.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using System.IO;

namespace EmployeeSpy.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }

        public static IApplicationBuilder UseLoggingMiddleware(this IApplicationBuilder builder, ILoggerFactory loggerFactory)
        {
            var path = Directory.GetCurrentDirectory();
            loggerFactory.AddFile($"{path}\\Logs\\Log.txt");

            return builder.UseMiddleware<LoggingMiddleware>();
        }
    }
}
