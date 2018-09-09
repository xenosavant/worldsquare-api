using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Data.ViewModels
{
    public class CurrencyAmountViewModel
    {
        public int CurrencyTypeId { get; set; }

        public decimal Amount { get; set; }
    }
}
