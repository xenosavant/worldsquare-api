using Stellmart.Api.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Data.ViewModels
{
    public class ListingViewModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public MessageThread Thread { get; set; }

        IEnumerable<InventoryItem> InventoryItems { get; set; }
    }
}
