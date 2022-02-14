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
        private IConfiguration ConfigService { get; set; }
        
        public Startup(IConfiguration configService)
        {
            ConfigService = configService;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<MessageOptions>(ConfigService.GetSection("Location"));
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();                
            }
            app.UseRouting();

            app.UseMiddleware<LocationMiddleware>();
            
            app.Use(async (context, next) =>
            {
                string defaultDebug = ConfigService["Logging:LogLevel:Default"];
                await context.Response.WriteAsync($"The config settings is: {defaultDebug}");
                string environ = ConfigService["ASPNETCORE_ENVIRONMENT"];
                await context.Response.WriteAsync($"\nThe env setting is: {environ}");
                string wsID = ConfigService["WebService:Id"];
                string wsKey = ConfigService["WebService:Key"];
                await context.Response.WriteAsync($"\nThe secret ID is: {wsID}");
                await context.Response.WriteAsync($"\nThe secret Key is: {wsKey}");
            });
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World");
                });
            });
        }
    }
}
