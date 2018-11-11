using Stellmart.Api.Context.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities
{
    public class ShipmentTracker : Entity<int>
    {
        public string SecretSigningKey { get; set; }

        public string TrackingId { get; set; }

        public int SignatureId { get; set; }

        public virtual SecretSignature Signature { get; set; }
    }
}
