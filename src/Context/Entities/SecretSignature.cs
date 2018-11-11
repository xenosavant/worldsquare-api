using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities
{
    // This represents a signature by secret key 
    // (could be a shipment tracker or a buyer secret key)
    public class SecretSignature : Signature
    {
        public string SecretKeyHash { get; set; }

        public virtual ShipmentTracker Tracker { get; set; }
    }
}
