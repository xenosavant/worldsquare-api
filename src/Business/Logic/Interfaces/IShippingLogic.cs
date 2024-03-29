﻿using Stellmart.Api.Context.Entities;
using Stellmart.Api.Data.Shipping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Logic.Interfaces
{
    public interface IShippingLogic
    {
        Task<bool> MarkShipmentDelivered(string trackingId);
        Task<ProductShipment> CreateAsync(ShipPackageData data);
    }
}
