using Stellmart.Api.Context.Entities.ReadOnly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Data.ViewModels
{
    public class CurrencyAmountViewModel
    {
        public Currency Currency { get; set; }

        public double Amount { get; set; }
    }
}
