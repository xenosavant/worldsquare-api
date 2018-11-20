using Stellmart.Api.Context.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities
{
    public class OrderItem : Entity<int>
    {
        public int? InventoryItemId { get; set; }
        public int? TradeItemId { get; set; }
        public int? ContractId { get; set; }
        public int? ProductShipmentId { get; set; }
        public int OrderId { get; set; }
        public int StoreId { get; set; }
        public virtual InventoryItem InventoryItem { get; set; }
        public virtual TradeItem TradeItem { get; set; }
        public virtual Order Order { get; set; }
        public virtual ProductShipment Shipment { get; set; }
        public virtual Contract Contract { get; set; }
    }
}
