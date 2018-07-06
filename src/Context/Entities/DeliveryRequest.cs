using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities
{
    public class DeliveryRequest : ServiceRequest
    {
        public virtual ProductShipment Shipment { get; set; }
    }
}
