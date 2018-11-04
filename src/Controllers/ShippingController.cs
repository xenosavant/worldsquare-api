using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stellmart.Api.Data.Shipping;
using Stellmart.Api.Services.Interfaces;

namespace Stellmart.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ShippingController : BaseController
    {
        private readonly IShippingService _shippingService;

        public ShippingController(IShippingService shippingService)
        {
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
        [Route("GetAllShippingCarriers")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult ShipPackage([FromBody] ShipPackageRequest request)
        {
            if (request.ShippingCarrierType != null && request.TrackingId != null)
            {
                var trackerResponse = _shippingService.GenerateShippingTracker(request.ShippingCarrierType, request.TrackingId);
                // create tracker
            }
            // sign transaction

        }


    }
}