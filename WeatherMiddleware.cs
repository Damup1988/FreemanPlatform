using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Platform.Services;

namespace Platform
{
    public class WeatherMiddleware
    {
        private readonly RequestDelegate _next;
        private IResponseFormatter _formatter;

        public WeatherMiddleware(RequestDelegate next, IResponseFormatter formatter)
        {
            _next = next;
            _formatter = formatter;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path == "/middleware/class")
            {
                await _formatter.Format(context, "Middleware Class: It is running in London");
            }
            else
            {
                await _next(context);
            }
        }
    }
}