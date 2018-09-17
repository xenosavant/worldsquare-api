using Stellmart.Api.Context.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities
{
    public class Order : AuditableEntity<int>
    {
        public int OnlineSaleId { get; set; }
        public virtual OnlineSale Sale { get; set; }
        public virtual ICollection<ProductShipment> Shipments { get; set; }
    }
}
