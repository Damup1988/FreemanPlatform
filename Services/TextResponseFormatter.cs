using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Platform.Services
{
    public class TextResponseFormatter : IResponseFormatter
    {
        private int _responseCounter = 0;
        private static TextResponseFormatter _shared;

        public static TextResponseFormatter SingleTone
        {
            get { return _shared ??= new TextResponseFormatter(); }
        }

        public async Task Format(HttpContext context, string content)
        {
            await context.Response.WriteAsync($"Response {++_responseCounter}:\n{content}");
        }
    }
}