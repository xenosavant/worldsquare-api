using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities
{
    public class ShipmentTracker
    {
        public string SecretSigningKey { get; set; }

        public string TrackingId { get; set; }

        public int TransactionId { get; set; }

        public virtual PreTransaction Transcaction { get; set; }
    }
}
