using Stellmart.Api.Context.Entities.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities
{
    public class LineItem : AuditableEntity<int>
    {
        [Required]
        public int InventoryItemId { get; set; }

        [Required]
        public int Quantity { get; set; }

        public int? ShippingManifestId { get; set; }

        public virtual ICollection<ShippingManifestLineItem> ShippingManifestLineItems { get; set; }

        public virtual InventoryItem InventoryItem { get; set; }
    }
}
