using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Stellmart.Api.Business.Logic.Interfaces;
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
    public class InventoryItemController : BaseController
    {
        private readonly IInventoryItemLogic _inventoryItemLogic;
        private readonly IMapper _mapper;

        public InventoryItemController(IInventoryItemLogic inventoryItemLogic, IMapper mapper)
        {
            _inventoryItemLogic = inventoryItemLogic;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<InventoryItemViewModel>> Post([FromBody] InventoryItemViewModel item)
        {
            if (!ModelState.IsValid || item.ListingId == null)
            {
                return BadRequest();
            }
            var entity = _mapper.Map<InventoryItem>(item);
            var newItem = await _inventoryItemLogic.CreateAndSaveAsync(UserId, entity);
            return CreatedAtRoute("GetListing", new { id = newItem.Id }, _mapper.Map<InventoryItemViewModel>(newItem));
        }

        [HttpPatch]
        [Route("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<InventoryItemViewModel>> Patch(int id, [FromBody] Delta<InventoryItem> delta)
        {
            var item = await _inventoryItemLogic.GetById(id, "Listing.OnlineStore,Price");
            if (item.Listing?.OnlineStore?.UserId != UserId || item.ListingId == null)
            {
                return Unauthorized();
            }
            return _mapper.Map<InventoryItemViewModel>(await _inventoryItemLogic.UpdateAndSaveAsync(UserId, item, delta));
        }

        [HttpDelete]
        [Route("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Delete(int id)
        {
            var item = await _inventoryItemLogic.GetById(id, "Listing.OnlineStore");
            if (item.Listing.OnlineStore.UserId != UserId)
            {
                return Unauthorized();
            }
            await _inventoryItemLogic.DeleteAsync(item);
            return NoContent();
        }



    }
}
