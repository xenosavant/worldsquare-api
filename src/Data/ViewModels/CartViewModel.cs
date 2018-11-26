using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Data.ViewModels
{
    public class CartViewModel
    {
        public List<LineItemViewModel> LineItems { get; set; }
    }
}
