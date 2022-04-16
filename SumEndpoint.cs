using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Caching.Distributed;
using Platform.Models;
using Platform.Services;

namespace Platform
{
    public class SumEndpoint
    {
        public async Task Endpoint(HttpContext context, CalculationContext calculationContext)
        {
            int count = int.Parse((string)context.Request.RouteValues["count"] ?? string.Empty);
            long total = calculationContext.Calculations.FirstOrDefault(c => c.Count == count)?.Result ?? 0;
            if (total == 0)
            {
                for (int i = 0; i < count; i++)
                {
                    total += i;
                }

                calculationContext.Calculations!.Add(new Calculation()
                {
                    Count = count,
                    Result = total
                });
                await calculationContext.SaveChangesAsync();
            }

            string totatlString = $"({DateTime.Now.ToLongTimeString()}) {total}";
            await context.Response.WriteAsync($"({DateTime.Now.ToLongTimeString()}) Totatl for {count}"
            + $" values:\n{totatlString}\n");
        }
    }
}