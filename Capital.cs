using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Platform
{
    public static class Capital
    {
        public static async Task Endoiint(HttpContext context)
        {
            string capital = null;
            string country = context.Request.RouteValues["country"] as string;
            switch ((country ?? "").ToLower())
            {
                case "uk":
                    capital = "London";
                    break;
                case "france":
                    capital = "Paris";
                    break;
                case "russia":
                    capital = "Moscow";
                    break;
                case "monaco":
                    context.Response.Redirect($"/population/{country}");
                    return;
            }

            if (capital != null)
            {
                await context.Response
                    .WriteAsync($"{capital} is the capital of {country}");
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
            }
        }
    }
}