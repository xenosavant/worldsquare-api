using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Stellmart.Api.Business.Logic.Interfaces;
using Stellmart.Api.Data.OnlineSale;
using Stellmart.Api.Data.Enums;
using System.Threading.Tasks;
using Stellmart.Api.Business.Managers.Interfaces;
using Stellmart.Api.Services.Interfaces;
using System.Linq;
using Stellmart.Api.Context.Entities;

namespace Stellmart.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class OnlineSaleController : AuthorizedController
    {
        private readonly IOnlineSaleLogic _saleLogic;
        private readonly IUserDataManager _userManager;
        private readonly IPaymentService _paymentService;
        private readonly IMapper _mapper;

        public OnlineSaleController(IMapper mapper, IOnlineSaleLogic saleLogic, IPaymentService paymentService)
        {
            _mapper = mapper;
            _saleLogic = saleLogic;
            _paymentService = paymentService;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<OnlineSaleResponse>> Checkout([FromBody] OnlineSaleRequest request)
        {
            OnlineSaleResponse response = new OnlineSaleResponse();

            var user = await _userManager.GetByIdAsync(UserId, "UserPaymentMethods");

            OnlineSaleData checkoutData = await _saleLogic.Checkout(user, request.Password);

            if (!checkoutData.Success)
            {
                return BadRequest();
            }

            var validationResult = await _saleLogic.ValidatePayment(user, response.Orders.Select(o => o.Sale).ToList(), request.Password, request.PaymentMethodId);

            if (validationResult == PaymentResult.InsufficentFunds)
            {
                response.PaymentStatus = (int)PaymentResult.InsufficentFunds;
                return response;
            }

            var result = await _saleLogic.MakePayment(user, response.Orders.Select(o => o.Sale).ToList(), request.Password, request.PaymentMethodId);
            response.PaymentStatus = (int)result;
            response.Orders = checkoutData.Orders;
            return Ok(response);
        }
    }
}
