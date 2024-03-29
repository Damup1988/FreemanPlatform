﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Platform.Services
{
    public class GuidService : IResponseFormatter
    {
        private readonly Guid _guid = Guid.NewGuid();
        public async Task Format(HttpContext context, string content)
        {
            await context.Response.WriteAsync($"Guid: {_guid}\n{content}");
        }
    }
}