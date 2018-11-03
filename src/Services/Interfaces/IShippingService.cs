using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Services.Interfaces
{
    public interface IShippingService
    {
        void MarkShipmentAsDelivered(string trackingId);
        string GenerateTrackingObject(int carrierId, string trackingId);
        string GeneratePostageLabelUri();
        List<string> GetAllCarrierTypes();

    }
}
