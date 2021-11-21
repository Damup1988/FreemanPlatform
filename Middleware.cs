using System.Drawing;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Platform
{
    public class QueryStringMiddleware
    {
        private readonly RequestDelegate? _next;

        public QueryStringMiddleware()
        {
            // do nothing
        }

        public QueryStringMiddleware(RequestDelegate nextDelegate)
        {
            _next = nextDelegate;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Method == HttpMethods.Get && context.Request.Query["custom"] == "true")
            {
                await context.Response.WriteAsync("Class-based Middleware \n");
            }
            
            if (_next != null)
            {
                await _next(context);
            }
        }
    }

    public class QueryStringMiddlewareFalse
    {
        private readonly RequestDelegate _next;

        public QueryStringMiddlewareFalse(RequestDelegate nextDelegate)
        {
            _next = nextDelegate;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Method == HttpMethods.Get && context.Request.Query["custom"] == "false")
            {
                await context.Response.WriteAsync("2nd Class-based Middleware \n");
            }

            await _next(context);
        }
    }

    public class LocationMiddleware
    {
        private RequestDelegate _next;
        private MessageOptions _optons;

        public LocationMiddleware(RequestDelegate nextDelegate, IOptions<MessageOptions> opts)
        {
            _next = nextDelegate;
            _optons = opts.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path == "/location")
            {
                await context.Response.WriteAsync($"{_optons.CityName}, {_optons.CountryName}");
            }
            else
            {
                await _next(context);
            }
        }
    }
}