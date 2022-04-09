using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;

namespace Platform
{
    public class ConsentMiddleware
    {
        private readonly RequestDelegate _next;

        public ConsentMiddleware(RequestDelegate nextDelegate)
        {
            _next = nextDelegate;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path == "/consent")
            {
                ITrackingConsentFeature consentFeature = context.Features.Get<ITrackingConsentFeature>();
                if (!consentFeature.HasConsent)
                {
                    consentFeature.GrantConsent();
                }
                else
                {
                    consentFeature.WithdrawConsent();
                }

                await context.Response.WriteAsync(consentFeature.HasConsent
                    ? "Consent Granted \n"
                    : "Consent Withdrawn\n");
            }

            await _next(context);
        }
    }
}