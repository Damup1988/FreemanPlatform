using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Platform
{
    public class WeatherEndpoint
    {
        public static async Task Endpoint(HttpContext context)
        {
            await context.Response.WriteAsync("Endpoint Class: It is cloudy in Milan");
        }
    }
}