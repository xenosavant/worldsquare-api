using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stellmart.Api.Business.Managers.Interfaces;
using Stellmart.Api.Data.Shipping;
using Stellmart.Api.Services.Interfaces;

namespace Stellmart.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ShippingController : BaseController
    {
        private readonly IShippingService _shippingService;
        private readonly ISecretKeyDataManager _secretKeyManager;
        private readonly IShipmentTrackerDataManager _trackerManager;
        private readonly ISignatureDataManager _signatureManager;
        

        public ShippingController(IShippingService shippingService,
            ISecretKeyDataManager secretKeyManager,
            IShipmentTrackerDataManager trackerManager,
            ISignatureDataManager signatureManager
            )
        {
            _secretKeyManager = secretKeyManager;
            _trackerManager = trackerManager;
            _signatureManager = signatureManager;
            _shippingService = shippingService;
        }

        [HttpPost]
        [Route("GetAllShippingCarriers")]
        [ProducesResponseType(200)]
        public ActionResult<List<string>> GetAllShippingCarriers()
        {
            return Ok(_shippingService.GetAllCarrierTypes());
        }

        [HttpPost]
        [Route("ShipPackage")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> ShipPackage([FromBody] ShipPackageRequest request)
        {
            var signature = await _signatureManager.GetAsync(request.SignatureId);
            if (request.ShippingCarrierType != null && request.TrackingId != null)
            {
                var secretKey = await _secretKeyManager.GetBuyerSecretKeyByOrderId(request.OrderId);
                var trackerResponse =_shippingService.GenerateShippingTracker(request.SignatureId, request.ShippingCarrierType, request.TrackingId);
                if (trackerResponse.Error == true)
                {
                    return BadRequest();
                }
                var tracker = _trackerManager.CreateAsync(secretKey.Key, trackerResponse.TackingId, request.SignatureId);
            }
            // TODO: sign transaction
            return Ok();
        }


    }
}