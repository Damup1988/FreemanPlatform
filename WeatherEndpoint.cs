using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Platform.Services;

namespace Platform
{
    public class WeatherEndpoint
    {
        //private readonly IResponseFormatter _formatter;

        // public WeatherEndpoint(IResponseFormatter formatter)
        // {
        //     _formatter = formatter;
        // }
        
        public async Task Endpoint(HttpContext context, IResponseFormatter _formatter)
        {
            await _formatter.Format(context, "Endpoint Class: It is cloudy in Milan");
        }
    }
}