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
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Platform.Services;

namespace Platform
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(opts =>
            {
                opts.CheckConsentNeeded = context => true;
            });

            services.AddDistributedMemoryCache();

            services.AddSession(optiopns =>
            {
                optiopns.IdleTimeout = TimeSpan.FromMinutes(30);
                optiopns.Cookie.IsEssential = true;
            });
        }
        
        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            app.UseCookiePolicy();
            app.UseMiddleware<ConsentMiddleware>();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/cookie", async context =>
                {
                    int counter1 = int.Parse(context.Request.Cookies["counter1"] ?? "0") + 1;
                    context.Response.Cookies.Append("counter1", counter1.ToString(),
                        new CookieOptions
                        {
                            MaxAge = TimeSpan.FromMinutes(30),
                            IsEssential = true
                        });
                    int counter2 = int.Parse(context.Request.Cookies["counter2"] ?? "0") + 1;
                    context.Response.Cookies.Append("counter2", counter1.ToString(),
                        new CookieOptions
                        {
                            MaxAge = TimeSpan.FromMinutes(30)
                        });
                    await context.Response.WriteAsync($"Counter1: {counter1}, Counter2: {counter2}");
                });

                endpoints.MapGet("clear", context =>
                {
                    context.Response.Cookies.Delete("counter1");
                    context.Response.Cookies.Delete("counter2");
                    context.Response.Redirect("/");
                    return Task.CompletedTask;
                });
                
                endpoints.MapFallback(async context =>
                    await context.Response.WriteAsync("Hello world!"));
            });
        }
    }
}
