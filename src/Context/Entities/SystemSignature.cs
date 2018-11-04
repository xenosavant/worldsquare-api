using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities
{
    public class SystemSignature : Signature
    {
        public virtual ShipmentTracker Tracker { get; set; }
    }
}
