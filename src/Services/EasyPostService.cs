using Microsoft.Extensions.Options;
using Stellmart.Api.Data.Settings;
using Stellmart.Api.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyPost;
using Stellmart.Api.Data.Shipping;

namespace Stellmart.Api.Services
{
    public class EasyPostService : IShippingService
    {

        public EasyPostService(IOptions<EasyPostSettings> settings)
        {
            ClientManager.SetCurrent("EZAK91b75125309449ce815cf3b2534dd39fl7SqEmv8sZIUSHbE7O4FZg");
        }

        public string GeneratePostageLabelUri()
        {
            throw new NotImplementedException();
        }

        public ShippingTrackerResponse GenerateShippingTracker(string carrierId, string trackingId)
        {
            var response = new ShippingTrackerResponse();
            try
            {
                response.TackingId = Tracker.Create(carrierId, trackingId).id;
            }
            catch
            {
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
