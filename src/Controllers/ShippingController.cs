using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stellmart.Api.Business.Managers.Interfaces;
using Stellmart.Api.Data.Shipping;
using Stellmart.Api.Services.Interfaces;
using EasyPost;
using Stellmart.Api.Business.Logic.Interfaces;
using Stellmart.Api.Data.Contract;

namespace Stellmart.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ShippingController : AuthorizedController
    {
        private readonly IShippingService _shippingService;
        private readonly IShipmentTrackerDataManager _trackerManager;
        private readonly ISignatureDataManager _signatureManager;
        private readonly IShippingLogic _shippingLogic;
        private readonly IContractService _contractService;
        private readonly IUserDataManager _userDataManager;


        public ShippingController(IShippingService shippingService,
            IShipmentTrackerDataManager trackerManager,
            ISignatureDataManager signatureManager,
            IShippingLogic shippingLogic,
            IContractService contractService,
            IUserDataManager userDataManager
            )
        {
            _trackerManager = trackerManager;
            _signatureManager = signatureManager;
            _shippingService = shippingService;
            _shippingLogic = shippingLogic;
            _contractService = contractService;
            _userDataManager = userDataManager;
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
            if (request.ShippingCarrierType != null && request.TrackingId != null)
            {
                var trackerResponse = _shippingService.GenerateShippingTracker(request.ShippingCarrierType, request.TrackingId);
                if (trackerResponse.Error == true)
                {
                    return BadRequest();
                }
                await _shippingLogic.CreateAsync(new ShipPackageData(request)
                {
                    EasyPostTrackerId = trackerResponse.TackingId,
                    CurrentUserId = UserId
                });
            }
            else
            {
                // In v2 we will allow this condition
                return BadRequest();
            }            
            return Ok();
        }

        [HttpPost]
        [Route("WebHook")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> WebHook([FromBody] Event e)
        {
            if (e == null || e.description == null)
            {
                return BadRequest();
            }
            if (e.description == "tracker.updated")
            {
                var tracker = (Tracker)e.result["Tracker"];
                if (tracker.status == "delivered")
                {
                    var success = await _shippingLogic.MarkShipmentDelivered(tracker.id);
                    if (success)
                    {
                        return Ok();
                    }
                    else
                    {
                        return StatusCode(500);
                    }
                }
                // TODO: handle other shipment states
                return Ok();
            }
            else
            {
                return Ok();
            }
        }
    }
}