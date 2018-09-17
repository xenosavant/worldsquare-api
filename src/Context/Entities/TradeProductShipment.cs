using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities
{
    public class TradeProductShipment
    {
        public int TradeId { get; set; }
        public int ProductShipmentId { get; set; }
        public virtual Trade Trade { get; set; }
        public virtual ProductShipment ProductShipment { get; set; }
    }
}
