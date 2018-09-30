using System;
using System.IO;
using System.Reflection;

namespace Stellmart.Api.Business.Helpers
{
    public static class GeoIpHelper
    {
        public static string GetGeoIpDatabaseFilename()
        {
            // get the file path of the executing assembly
            Uri uri = new Uri(Assembly.GetExecutingAssembly()
                .CodeBase);

            // and then figure out the directory we're in
            string directory = Path.GetDirectoryName(uri.LocalPath);

            if (directory == null)
            {
                throw new FileNotFoundException("Could not get local directory path to load geoip database");
            }

            // and assemble the path to the maxmind database
            return Path.Combine(directory, "Resources", "GeoLite2-Country.mmdb");
        }
    }
}
