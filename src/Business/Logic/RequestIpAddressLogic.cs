using Microsoft.AspNetCore.Http;
using Stellmart.Api.Business.Logic.Interfaces;
using System.Net;

namespace Stellmart.Api.Business.Logic
{
    public class RequestIpAddressLogic : IRequestIpAddressLogic
    {
        // format is - X-Forwarded-For: client, proxy1, proxy2[3]
        private const string ForwardedFor = "X-Forwarded-For";

        public IPAddress Get(HttpContext httpContext)
        {
            // no http context, therefore no address!
            if (httpContext == null)
            {
                return null;
            }

            // check for the ForwardedFor header
            if (httpContext.Request.Headers.ContainsKey(ForwardedFor))
            {
                string forwardedFor = httpContext.Request.Headers[ForwardedFor];

                if (!string.IsNullOrEmpty(forwardedFor))
                {
                    if (IPAddress.TryParse(forwardedFor.Split(',')[0], out IPAddress clientAddress))
                    {
                        return clientAddress;
                    }
                }
            }

            return httpContext.Connection?.RemoteIpAddress;
        }
    }
}
