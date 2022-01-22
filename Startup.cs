using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Platform.Services;

namespace Platform
{
    public class Startup
    {
        private IConfiguration _configuration;
        
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IResponseFormatter, TimeResponseFormatter>();
            services.AddScoped<ITimeStamper, DefaultTimeStamper>();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseRouting();
            app.UseMiddleware<WeatherMiddleware>();

            app.Use(async (context, next) =>
            {
                if (context.Request.Path == "/middleware/function")
                {
                    IResponseFormatter formatter = context.RequestServices.GetService<IResponseFormatter>();
                    await formatter.Format(
                        context, "Middleware Function: It is snowing in Chicago");
                }
                else
                {
                    await next();
                }
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapEndpoint<WeatherEndpoint>("/endpoint/class");
                endpoints.MapGet("/endpoint/function", async context =>
                {
                    IResponseFormatter formatter = context.RequestServices.GetService<IResponseFormatter>();
                    await formatter.Format(
                    context, "Endpoint Function: It is sunny in LA");
                });
            });
        }
    }
}
