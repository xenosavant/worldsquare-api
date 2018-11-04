using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Data.Shipping
{
    public class ShippingTrackerResponse
    {
        public bool Error { get; set; } = false;
        public string TackingId { get; set; }
    }
}
