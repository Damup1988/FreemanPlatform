using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HostFiltering;
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

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.IsEssential = true;
            });

            services.AddHsts(opts =>
            {
                opts.MaxAge = TimeSpan.FromDays(1);
                opts.IncludeSubDomains = true;
            });

            services.Configure<HostFilteringOptions>(opts =>
            {
                opts.AllowedHosts.Clear();
                opts.AllowedHosts.Add("*.example.com");
            });
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseExceptionHandler("/error.html");
            if (env.IsProduction())
            {
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStatusCodePages("text/html", ResponseStrings.DefaultResponse);
            app.UseCookiePolicy();
            app.UseStaticFiles();
            app.UseMiddleware<ConsentMiddleware>();
            app.UseSession();

            app.Use(async (context, next) =>
            {
                if (context.Request.Path == "/error")
                {
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                }
                else
                {
                    await next();
                }
            });
            
            app.Run(context => throw new Exception("Something has gone wrong"));
        }
    }
}
