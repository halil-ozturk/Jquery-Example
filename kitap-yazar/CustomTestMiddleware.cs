using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace kitap_yazar
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class CustomTestMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomTestMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Path.Value.Contains("/home/test"))
            {
                httpContext.Response.StatusCode = 401;
                return _next(httpContext);
            }

            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class CustomTestMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomTestMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomTestMiddleware>();
        }
    }
}
