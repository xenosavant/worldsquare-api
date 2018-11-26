using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stellmart.Api.Business.Logic.Interfaces;
using Stellmart.Api.Business.Managers.Interfaces;
using Stellmart.Api.Context.Entities;
using Stellmart.Api.Data;
using Stellmart.Api.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    //[Authorize]
    public class CartController : AuthorizedController
    {
        private readonly ICartDataManager _dataManager;
        private readonly ICartLogic _cartLogic;
        private readonly ILineItemDataManager _lineItemDataManager;
        private readonly IMapper _mapper;

        public CartController(ICartDataManager dataManager,
            ILineItemDataManager lineItemDataManager,
            IMapper mapper,
            ICartLogic cartLogic)
        {
            _dataManager = dataManager;
            _lineItemDataManager = lineItemDataManager;
            _mapper = mapper;
            _cartLogic = cartLogic;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<CartViewModel>> AddItem([FromBody] InventoryItemDetailViewModel item)
        {
            var cart = await _cartLogic.AddItemToCart(item, UserId);
            return Ok(_mapper.Map<CartViewModel>(await _dataManager.SaveAsync(cart)));
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<CartViewModel>> Get()
        {
            var cart = await _dataManager.GetAsync(UserId);
            if (cart == null)
            {
                cart = await _dataManager.CreateAsync(null, UserId);
            }
            return Ok(_mapper.Map<CartViewModel>(cart));
        }

        [HttpPatch]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<LineItemViewModel>> Patch(int id, [FromBody] Delta<LineItem> delta)
        {
            var item = await _lineItemDataManager.GetAsync(id);
            if (item.Cart.UserId != UserId)
            {
                return Unauthorized();
            }
            await _lineItemDataManager.UpdateAsync(item, delta);
            return Ok(_mapper.Map<LineItemViewModel>(item));
        }

        [HttpDelete]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> RemoveItem(int lineItemId)
        {
            var lineItem = await _lineItemDataManager.GetAsync(lineItemId);
            if (lineItem.Cart.UserId != UserId)
            {
                return BadRequest();
            }
            await _lineItemDataManager.DeleteAsync(lineItem);
            return NoContent();
        }
    }
}
