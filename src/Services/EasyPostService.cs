using Microsoft.Extensions.Options;
using Stellmart.Api.Data.Settings;
using Stellmart.Api.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyPost;
using Stellmart.Api.Data.Shipping;
using Stellmart.Api.Business.Managers.Interfaces;
using Stellmart.Api.Context.Entities;

namespace Stellmart.Api.Services
{
    public class EasyPostService : IShippingService
    {

        private readonly IShipmentTrackerDataManager _trackerDataManager;
        private readonly ISignatureDataManager _signatureDataManager;
        

        public EasyPostService(IOptions<EasyPostSettings> settings, ISignatureDataManager signatureDataManager)
        {
            ClientManager.SetCurrent(settings.Value.ApiKey);
            _signatureDataManager = signatureDataManager;
        }

        public string GeneratePostageLabelUri()
        {
            throw new NotImplementedException();
        }

        public ShippingTrackerResponse GenerateShippingTracker(int signatureId, string carrierId, string trackingId)
        {
            var response = new ShippingTrackerResponse();
            try
            {
                var result = Tracker.Create(carrierId, trackingId);
                response.TackingId = result.id;
            }
            catch (Exception e)
            {
                var error = e;
                response.Error = true;
            }
            return response;
        }

        public List<string> GetAllCarrierTypes()
        {
            //return CarrierType.All().Select(ct => ct.readable.Replace(" ", "")).ToList();
            // This call only works with production api, so use mock data for now
            return new List<string>() {
                "AmazonMws",
                "APC",
                "Aramex",
                "Arrowxl",
                "Asendia",
                "AsendiaEurope",
                "AsendiaHK",
                "AustraliaPost",
                "Axlehire",
                "Boxberry",
                "CanadaPost",
                "Canpar",
                "ColisPrive",
                "ColumbusLastMile",
                "CouriersPlease",
                "DaiPost",
                "Deliv",
                "DeutschePost",
                "DhlEcommerce",
                "DhlEcommerceAsia",
                "DhlExpress",
                "DhlParcel",
                "DHLGMI",
                "Dicom",
                "DirectLink",
                "Doorman",
                "DPD",
                "DPDUk",
                "Estafeta",
                "Estes",
                "Fastway",
                "Fedex",
                "FedexMailview",
                "FedexSamedayCity",
                "FedexSmartpost",
                "FromParcel",
                "Globegistics",
                "Gso",
                "Hermes",
                "HongKongPost",
                "InterlinkExpress",
                "Lasership",
                "Liefery",
                "Lso",
                "Network4",
                "Newgistics",
                "NinjaVan",
                "Norco",
                "Ontrac",
                "OntracDirectPost",
                "OrangeDS",
                "OsmWorldwide",
                "ParcelForce",
                "PassportGlobal",
                "Pilot",
                "Postmates",
                "Postnl",
                "Purolator",
                "RoyalMail",
                "RRDonnelley",
                "Seko",
                "SingaporePost",
                "Speedee",
                "SprintShip",
                "StarTrack",
                "Tforce",
                "UDS",
                "UPS",
                "UpsIparcel",
                "UpsMailInnovations",
                "USPS",
                "Yodel"};
    }

        public void MarkShipmentAsDelivered(string trackingId)
        {
            throw new NotImplementedException();
        }
    }
}
