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
    public class ItemMetaDataController : ControllerBase
    {
        private readonly IItemMetaDataLogic _itemMetaDataLogic;
        private readonly IMapper _mapper;

        public ItemMetaDataController(IItemMetaDataLogic itemMetaDataLogic, IMapper mapper)
        {
            _itemMetaDataLogic = itemMetaDataLogic;
            _mapper = mapper;
        }


        [HttpPatch]
        [Route("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ItemMetaDataViewModel>> Patch(int id, [FromBody] Delta<ItemMetaData> delta)
        {
            var item = await _itemMetaDataLogic.GetById(id, "Listing.OnlineStore");
            if (item.Listing.OnlineStore.UserId != 1)
            {
                return Unauthorized();
            }
            return _mapper.Map<ItemMetaDataViewModel>(await _itemMetaDataLogic.UpdateAndSaveAsync(1, item, delta));
        }

    }
}
