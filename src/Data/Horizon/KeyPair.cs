using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Data.Horizon
{
    public class KeyPair
    {
        public string Public_Key { get; set; }
        public string Encoded_Secret { get; set; }
        /* ToDo ; maybe add function to encode secret */
    }
}
