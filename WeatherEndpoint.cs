using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Platform.Services;

namespace Platform
{
    public class WeatherEndpoint
    {
        public static async Task Endpoint(HttpContext context)
        {
            IResponseFormatter formatter =
                context.RequestServices.GetRequiredService<IResponseFormatter>();
            await formatter.Format(context, "Endpoint Class: It is cloudy in Milan");
        }
    }
}