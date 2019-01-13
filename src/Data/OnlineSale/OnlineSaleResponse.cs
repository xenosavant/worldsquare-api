using Stellmart.Api.Context.Entities;
using Stellmart.Api.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Data.OnlineSale
{
    public class OnlineSaleResponse
    {
        public int PaymentStatus { get; set; }
        public List<Order> Orders { get; set; }
    }
}
