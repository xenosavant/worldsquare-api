using Stellmart.Api.Context.Entities.Entity;
using Stellmart.Api.Context.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities
{
    public class LineItem : Entity<int>
    {
        public int? InventoryItemId { get; set; }
        public int? TradeItemId { get; set; }

        [Required]
        public int Quantity { get; set; }

        public int? ShippingManifestId { get; set; }

        private InventoryItem InventoryItem { get; set; }
        private TradeItem TradeItem { get; set; }

        public virtual ShippingManifest ShippingManifest { get; set; }

        public IItem Item { get { return InventoryItem == null ? (IItem)InventoryItem : (IItem)TradeItem; }
        }
    }
}
