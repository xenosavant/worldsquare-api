using Stellmart.Api.Context.Entities.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities
{
    public class ShippingManifest : Entity<int>
    {
        public virtual ICollection<LineItem> LineItems  { get; set; }

        public virtual ProductShipment Shipment { get; set; }
    }
}
