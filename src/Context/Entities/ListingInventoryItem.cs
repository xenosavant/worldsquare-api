using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities
{
    public class ListingInventoryItem
    {
        public int ListingId { get; set; }

        public int InventoryItemId { get; set; }

        public virtual Listing Listing { get; set; }

        public virtual InventoryItem InventoryItem { get; set; }
    }
}
