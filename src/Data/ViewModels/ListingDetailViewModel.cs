using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Data.ViewModels
{
    public class ListingDetailViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ItemConditionId { get; set; }
        public int OnlineStoreId { get; set; }

        public List<InventoryItemDetailViewModel> InventoryItems {get; set; }
    }
}
