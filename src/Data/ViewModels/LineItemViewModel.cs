using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Data.ViewModels
{
    public class LineItemViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? UnitTypeId { get; set; }
        public int ItemConditionId { get; set; }
        public int CurrencyTypeId { get; set; }
        public decimal CurrencyAmount { get; set; }
        public string Descriptors { get; set; }
        public int Quantity { get; set; }
    }
}
