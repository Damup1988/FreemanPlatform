using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Platform.Services
{
    public class TimeResponseFormatter : IResponseFormatter
    {
        private ITimeStamper _stamper;

        public TimeResponseFormatter(ITimeStamper stamper)
        {
            _stamper = stamper;
        }
        
        public async Task Format(HttpContext context, string content)
        {
            await context.Response.WriteAsync($"{_stamper.TimeStamp}: {content}");
        }
    }
}