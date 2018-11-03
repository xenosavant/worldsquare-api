using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
    }
}