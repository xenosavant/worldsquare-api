using System.Net;
using Microsoft.AspNetCore.Http;

namespace Stellmart.Api.Business.Logic.Interfaces
{
    /// <summary>
    ///     Manager for accessing IP Addresses from an Http Request
    /// </summary>
    public interface IRequestIpAddressManager
    {
        /// <summary>
        ///     Gets the IP Address of the client from the request, inspecting any proxy headers as appropriate.
        /// </summary>
        /// <param name="httpContext">the http context.</param>
        /// <returns>The IP Address.</returns>
        IPAddress Get(HttpContext httpContext);
    }
}
