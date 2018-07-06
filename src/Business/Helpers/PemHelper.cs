using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Helpers
{
    public class PemHelper
    {
        public static StreamReader LoadPemFromString(string cleanPem)
        {
            var pem = $"-----BEGIN RSA PRIVATE KEY-----\r\n{cleanPem}\r\n-----END RSA PRIVATE KEY-----";
            byte[] byteArray = Encoding.UTF8.GetBytes(pem);
            MemoryStream stream = new MemoryStream(byteArray);
            return new StreamReader(stream);
        }
    }
}
