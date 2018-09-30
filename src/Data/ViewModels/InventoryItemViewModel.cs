﻿using Stellmart.Api.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Data.ViewModels
{
    public class InventoryItemViewModel
    {
        public int? Id { get; set; }

        public int? ListingId { get; set; }

        public string Descriptors { get; set; }

        public string UPC { get; set; }

        public string SKU { get; set; }

        public int UnitsAvailable { get; set; }

        public int? UnitsSold { get; set; }

        public int? UnitsReturned { get; set; }

        public int CurrencyTypeId { get; set; }

        public decimal CurrencyAmount { get; set; }
    }
}
