using Stellmart.Api.Data.Shipping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Services.Interfaces
{
    public interface IShippingService
    {
        void MarkShipmentAsDelivered(string trackingId);
        ShippingTrackerResponse GenerateShippingTracker(int signatureId, string carrierId, string trackingId);
        string GeneratePostageLabelUri();
        List<string> GetAllCarrierTypes();

    }
}
