using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Data.ViewModels
{
    public class CurrencyViewModel
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public int Precision { get; set; }
    }
}
