using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Platform
{
    public class WeatherMiddleware
    {
        private readonly RequestDelegate _next;

        public WeatherMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path == "/middleware/class")
            {
                await context.Response.WriteAsync("Middleware Class: It is raining in London");
            }
            else
            {
                await _next(context);
            }
        }
    }
}