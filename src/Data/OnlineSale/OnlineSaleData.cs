using Stellmart.Api.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Data.OnlineSale
{
    public class OnlineSaleData
    {
        public bool Success { get; set; }
        public List<Order> Orders { get; set; }
    }
}
