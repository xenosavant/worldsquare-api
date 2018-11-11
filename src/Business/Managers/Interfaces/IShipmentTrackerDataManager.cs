using Stellmart.Api.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Managers.Interfaces
{
    public interface IShipmentTrackerDataManager
    {
        Task<ShipmentTracker> CreateAsync(string secretKey, string trackingId, int signatureId);
        Task<ShipmentTracker> GetAsync(string trackingId);
        Task<ShipmentTracker> UpdateAsync(ShipmentTracker tracker);
    }
}
