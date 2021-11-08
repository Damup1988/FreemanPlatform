using System.Drawing;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Platform
{
    public class QueryStringMiddleware
    {
        private readonly RequestDelegate _next;

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

            await _next(context);
        }
    }
}