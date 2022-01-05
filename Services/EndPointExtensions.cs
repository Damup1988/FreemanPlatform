using Microsoft.AspNetCore.Routing;
using Platform.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.AspNetCore.Builder
{
    public static class EndPointExtensions
    {
        public static void MapWeather(this IEndpointRouteBuilder app, string path)
        {
            IResponseFormatter formatter = app.ServiceProvider.GetService<IResponseFormatter>();
            app.MapGet(path, context => 
            Platform.WeatherEndpoint.Endpoint(context, formatter));
        }
    }
}