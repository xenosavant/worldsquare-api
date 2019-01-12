using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Data.ViewModels
{
    public class InventoryItemDetailViewModel
    {
        public int Id { get; set; }
        public string Variations { get; set; }
        public string CurrencyName { get; set; }
        public decimal CurrencyAmount { get; set; }
    }
}
