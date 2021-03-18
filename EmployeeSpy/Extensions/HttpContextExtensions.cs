using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace EmployeeSpy.Extensions
{
    public static class HttpContextExtensions
    {
        public static async Task<string> GetRequestBodyStringAsync(this HttpContext httpContext)
        {
            var body = string.Empty;
            if (httpContext.Request.ContentLength == null
                || !httpContext.Request.ContentLength.HasValue || httpContext.Request.ContentLength.Value == 0)
            {
                return await Task.FromResult<string>(body);
            }

            httpContext.Request.EnableBuffering();

            var length = httpContext.Request.ContentLength.Value;
            var buff = new char[length];
            using (var reader = new StreamReader(httpContext.Request.Body, Encoding.UTF8, true, 1024, true))
            {
                var i = await reader.ReadAsync(buff, 0, (int)length);
            }

            httpContext.Request.Body.Position = 0;
            return new string(buff);
        }
    }
}
