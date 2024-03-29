﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Stellmart.Api.Business.Logic;
using Stellmart.Api.Context.Entities;
using Stellmart.Api.Data;
using Stellmart.Api.Data.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stellmart.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class OnlineStoreController: AuthorizedController
    {
        private readonly IOnlineStoreLogic _storeLogic;
        private readonly IMapper _mapper;

        public OnlineStoreController(IOnlineStoreLogic storeLogic, IMapper mapper)
        {
            _storeLogic = storeLogic;
            _mapper = mapper;
        }

        //GET: api/onlinestore
       [HttpGet]
       [Route("")]
       [ProducesResponseType(200)]
       public async Task<ActionResult<IEnumerable<OnlineStoreViewModel>>> Get()
       {
            return _mapper.Map<List<OnlineStoreViewModel>>(await _storeLogic.GetAll());
       }

        //GET: api/onlinestore/1
        [HttpGet]
        [Route("{id:int}", Name = "GetOnlineStore")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<OnlineStoreViewModel>> GetSingle(int id)
        {
            var store = await _storeLogic.GetById(id);
            if (store == null)
            {
                return NotFound();
            }
            return _mapper.Map<OnlineStoreViewModel>(store);
        }

        //POST: api/onlinestore
        [HttpPost]
        [Route("")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<OnlineStoreViewModel>> Post([FromBody] OnlineStoreViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var store = await _storeLogic.Create(UserId, viewModel);
            return CreatedAtRoute("GetOnlineStore", new { id = store.Id }, _mapper.Map<OnlineStoreViewModel>(store));
        }

        //PATCH: api/onlinestore/1
        [HttpPatch]
        [Route("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<OnlineStoreViewModel>> Patch(int id, [FromBody] Delta<OnlineStore> delta)
        {
            var onlineStore = await _storeLogic.GetById(id);
            if (delta.ContainsKey("Verified"))
            {
                return BadRequest();
            }
            if (onlineStore.UserId != UserId)
            {
                return Unauthorized();
            }
            return _mapper.Map<OnlineStoreViewModel>(await _storeLogic.Update(UserId, onlineStore, delta));
        }

        //DELETE: api/onlinestore/1
        [HttpDelete]
        [Route("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Delete(int id)
        {
            var onlineStore = await _storeLogic.GetById(id);
            if (onlineStore.UserId != UserId)
            {
                return Unauthorized();
            }
            await _storeLogic.Delete(onlineStore);
            return NoContent();
        }
    }
}
