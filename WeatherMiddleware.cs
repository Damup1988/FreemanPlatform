using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Platform.Services;

namespace Platform
{
    public class WeatherMiddleware
    {
        private readonly RequestDelegate _next;

        public WeatherMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context,
            IResponseFormatter _formatter,
            IResponseFormatter _formatter2,
            IResponseFormatter _formatter3)
        {
            if (context.Request.Path == "/middleware/class")
            {
                await _formatter.Format(context, string.Empty);
                await _formatter2.Format(context, string.Empty);
                await _formatter3.Format(context, string.Empty);
            }
            else
            {
                await _next(context);
            }
        }
    }
}