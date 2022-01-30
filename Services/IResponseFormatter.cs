using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Platform.Services
{
    public interface IResponseFormatter
    {
        public bool RichOutput => false; 
        Task Format(HttpContext context, string content);
    }
}