using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using EmployeeSpy.Extensions;
using System.IO;
using System.Text;
using System.Diagnostics;

namespace EmployeeSpy.Middlewares
{
    public class LoggingMiddleware
    {
        private class RequestLogRecord
        {
            public string HttpMethod { get; set; }

            public string Path { get; set; }

            public string RequestBody { get; set; }

            public int ResponseStatusCode { get; set; }

            public long ExecutionMilliseconds { get; set; }
        }

        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public LoggingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<LoggingMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            var logRecord = new RequestLogRecord();
            logRecord.Path = context.Request.Path;
            logRecord.HttpMethod = context.Request.Method;
            logRecord.RequestBody = await context.GetRequestBodyStringAsync();
            
            var timer = new Stopwatch();
            timer.Start();

            await _next(context);

            timer.Stop();

            logRecord.ResponseStatusCode = context.Response.StatusCode;
            logRecord.ExecutionMilliseconds = timer.ElapsedMilliseconds;
            _logger.Log(LogLevel.Information, $"requestInfo: { logRecord.ToJson() }");
        }
    }
}
