using MaxMind.GeoIP2;
using Stellmart.Api.Business.Logic.Interfaces;
using System;
using System.Net;

namespace Stellmart.Api.Business.Logic
{
    public class CountryResolverLogic : ICountryResolverLogic
    {
        private readonly IGeoIP2DatabaseReader _geoIp2Database;

        public CountryResolverLogic(IGeoIP2DatabaseReader geoIp2Database)
        {
            this._geoIp2Database = geoIp2Database ?? throw new ArgumentNullException(nameof(geoIp2Database));
        }

        public string Resolve(IPAddress ipAddress)
        {
            var countryResponse = this._geoIp2Database.Country(ipAddress);

            if (countryResponse?.Country == null)
            {
                return string.Empty;
            }

            return string.IsNullOrEmpty(countryResponse.Country.IsoCode) ? string.Empty : countryResponse.Country.IsoCode;
        }
    }
}
