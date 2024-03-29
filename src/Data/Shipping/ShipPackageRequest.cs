﻿using Stellmart.Api.Context.Entities;
using Stellmart.Api.Data.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Data.Shipping
{
    public class ShipPackageRequest : SignatureRequest
    {
        public string TrackingId { get; set; }
        public string ShippingCarrierType { get; set; }
        public int OrderId { get; set; }
        public List<OrderItem> Items { get; set; }
    }
}
