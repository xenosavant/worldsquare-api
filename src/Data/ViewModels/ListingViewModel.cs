using Stellmart.Api.Context.Entities;
using Stellmart.Api.Context.Entities.ReadOnly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Data.ViewModels
{
    public class ListingViewModel
    {
        public int Id { get; set; }

        public int OnlineStoreId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int? UnitTypeId { get; set; }

        public virtual ItemMetaDataViewModel ItemMetaData { get; set; }

        public MessageThread Thread { get; set; }

        public IEnumerable<InventoryItemViewModel> InventoryItems { get; set; }
    }
}
