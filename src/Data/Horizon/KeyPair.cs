using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Data.Horizon
{
    public class KeyPair
    {
        public byte[] Public_Key { get; set; }
        public byte[] Encoded_Secret { get; set; }
        /* ToDo ; maybe add function to encode secret */
    }
}
