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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Platform.Models;
using Platform.Services;

namespace Platform
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedSqlServerCache(opts =>
            {
                opts.ConnectionString = _configuration["ConnectionStrings:CacheConnection"];
                opts.SchemaName = "dbo";
                opts.TableName = "DataCache";
            });
            services.AddResponseCaching();
            services.AddSingleton<IResponseFormatter, HtmlResponseFormatter>();
            services.AddDbContext<CalculationContext>(opts =>
            {
                opts.UseSqlServer(_configuration["ConnectionStrings:CalcConnection"]);
                opts.EnableSensitiveDataLogging();
            });
            services.AddTransient<SeedData>();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            IHostApplicationLifetime lifetime, SeedData seedData)
        {
            app.UseDeveloperExceptionPage();
            app.UseResponseCaching();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapEndpoint<SumEndpoint>("/sum/{count:int=1000000000}");
                
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync($"Hello world! Env: {env.EnvironmentName}");
                });
            });
            bool cmdLineInit = (_configuration["INITDB"] ?? "false") == "true";
            if (env.IsDevelopment())
            {
                seedData.SeedDataBase();
                if (cmdLineInit)
                {
                    lifetime.StopApplication();
                }
            }
        }
    }
}
