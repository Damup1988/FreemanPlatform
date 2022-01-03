#nullable enable
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Platform
{
    public class CountryRouteConstraint : IRouteConstraint
    {
        private static readonly string[] Countries = { "uk", "france", "monaco" };
        
        public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values,
            RouteDirection routeDirection)
        {
            string segmentValue = values[routeKey] as string ?? "";
            return Array.IndexOf(Countries, segmentValue.ToLower()) > -1;
        }
    }
}