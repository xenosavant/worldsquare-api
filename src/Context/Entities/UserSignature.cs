using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities
{
    // This represents a signature by the WorldSquare master key
    public class UserSignature : Signature
    {
        public int SignerId { get; set; }

        public virtual ApplicationUser Signer { get; set; }
    }
}
