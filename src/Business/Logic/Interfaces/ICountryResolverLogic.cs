using System.Net;

namespace Stellmart.Api.Business.Logic.Interfaces
{
    /// <summary>
    ///     GeoIP Country resolver.
    /// </summary>
    public interface ICountryResolverLogic
    {
        /// <summary>
        ///     Finds the country that an IP address resides in using a Geo IP db.
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        string Resolve(IPAddress ipAddress);
    }
}
