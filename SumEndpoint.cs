using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Platform
{
    public class SumEndpoint
    {
        public async Task Endpoint(HttpContext context)
        {
            int count = int.Parse((string)context.Request.RouteValues["count"] ?? string.Empty);
            long total = 0;

            for (int i = 0; i < count; i++)
            {
                total += i;
            }

            string totalString = $"({DateTime.Now.ToLongTimeString()}) {total}";
            await context.Response.WriteAsync($"({DateTime.Now.ToLongTimeString()}) Total for {count}"
            + $" values:\n{totalString}\n");
        }
    }
}