using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities
{
    public class DeliveryRequest : ServiceRequest
    {
        public int DestinationId { get; set; }

        public virtual Location Destination { get; set; }
    }
}
