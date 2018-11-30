using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Stellmart.Api.Data.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stellmart.Api.Data.Enums;
using Stellmart.Api.Business.Logic.Interfaces;

namespace Stellmart.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CheckoutController : AuthorizedController
    {
        private readonly ICheckoutLogic _checkoutLogic;
        private readonly IMapper _mapper;

        public CheckoutController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<CheckoutResponse>> Checkout([FromBody] CheckoutRequest request)
        {
            // TODO: check balance
            CheckoutResponse response = new CheckoutResponse()
            {
                Success = false
            };
            CheckoutData checkoutData = null;
            switch (request.PaymentTypeId)
            {
                case (int)PaymentsTypes.ManagedWallet:
                    checkoutData = await _checkoutLogic.ManagedCheckout(UserId, request.Password);
                    break;
                default:
                    return BadRequest();
            }
            response.Success = checkoutData.Success;
            response.Order = checkoutData.Order;
            return Ok(response);
        }
    }
}
