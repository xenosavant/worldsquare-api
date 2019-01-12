using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Stellmart.Api.Business.Logic.Interfaces;
using Stellmart.Api.Data.Checkout;
using Stellmart.Api.Data.Enums;
using System.Threading.Tasks;

namespace Stellmart.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CheckoutController : AuthorizedController
    {
        private readonly ICheckoutLogic _checkoutLogic;
        private readonly IMapper _mapper;

        public CheckoutController(IMapper mapper, ICheckoutLogic checkoutLogic)
        {
            _mapper = mapper;
            _checkoutLogic = checkoutLogic;
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
            response.Orders = checkoutData.Orders;
            return Ok(response);
        }
    }
}
