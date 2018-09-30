using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stellmart.Api.Context.Entities.ReadOnly;
using System.ComponentModel.DataAnnotations.Schema;
using Stellmart.Api.Context.Entities.Entity;
using Stellmart.Api.Context.Entities.Interfaces;
using Microsoft.Azure.Search.Models;
using Microsoft.Azure.Search;
using Stellmart.Api.Context.Entities.BaseEntity;

namespace Stellmart.Api.Context.Entities
{
    public class InventoryItem : UniqueEntity, IItem
    {
        public int? ListingId { get; set; }

        public int UnitPriceId { get; set; }

        public string Descriptors { get; set; }

        public string UPC { get; set; }

        public string SKU { get; set; }

        public int UnitsAvailable { get; set; }

        public int UnitsSold { get; set; }

        public int UnitsReturned { get; set; }

        public virtual CurrencyAmount Price { get; set; }

        public virtual Listing Listing { get; set; }

        public virtual LineItem LineItem { get; set; }

    }
}
