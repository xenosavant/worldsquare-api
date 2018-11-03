using Microsoft.Extensions.Options;
using Stellmart.Api.Data.Settings;
using Stellmart.Api.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyPost;

namespace Stellmart.Api.Services
{
    public class EasyPostService : IShippingService
    {

        public EasyPostService(IOptions<EasyPostSettings> settings)
        {
            ClientManager.SetCurrent(settings.Value.ApiKey);
        }

        public string GeneratePostageLabelUri()
        {
            throw new NotImplementedException();
        }

        public string GenerateTrackingObject(int carrierId, string trackingId)
        {
            throw new NotImplementedException();
        }

        public List<string> GetAllCarrierTypes()
        {
            
            return CarrierType.All().Select(ct => ct.type).ToList();
        }

        public void MarkShipmentAsDelivered(string trackingId)
        {
            throw new NotImplementedException();
        }
    }
}
