using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities.ReadOnly
{
    public class ShippingCarrier : LookupData
    {
        public virtual ICollection<ProductShipment> Shipments { get; set; }
    }
}
