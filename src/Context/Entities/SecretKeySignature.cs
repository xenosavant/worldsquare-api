using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities
{
    public class SecretKeySignature : UserSignature
    {
        public string SecretKeyHash { get; set; }
    }
}
