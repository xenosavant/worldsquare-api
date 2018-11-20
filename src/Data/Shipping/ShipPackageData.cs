using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Data.Shipping
{
    public class ShipPackageData : ShipPackageRequest
    {
        public ShipPackageData(ShipPackageRequest request)
        {
            TrackingId = request.TrackingId;
            ShippingCarrierType = request.ShippingCarrierType;
            OrderId = request.OrderId;
            Items = request.Items;
        }
        public string EasyPostTrackerId { get; set; }
        public int CurrentUserId { get; set; }
    }
}
