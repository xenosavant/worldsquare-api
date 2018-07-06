using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities
{
    public class UserSignature : Signature
    {
        public int SignerId { get; set; }

        public virtual ApplicationUser Signer { get; set; }
    }
}
