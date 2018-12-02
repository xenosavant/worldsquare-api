﻿using Stellmart.Api.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Data.Checkout
{
    public class CheckoutData
    {
        public bool Success { get; set; }
        public Order Order { get; set; }
    }
}