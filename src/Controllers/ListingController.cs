using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stellmart.Api.Business.Logic.Interfaces;
using Stellmart.Api.Context.Entities;
using Stellmart.Api.Context.Entities.ReadOnly;
using Stellmart.Api.Data.ModelBinders;
using Stellmart.Api.Data.ViewModels;
using Stellmart.Api.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Stellmart.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ListingController : BaseController
    {
        private readonly IListingLogic _listingLogic;
        private readonly IMapper _mapper;

        public ListingController(IListingLogic listingLogic, IMapper mapper)
        {
            _listingLogic = listingLogic;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<ListingViewModel>>> Get(
            [FromQuery] [ModelBinder(
            typeof(CommaDelimitedArrayModelBinder))]
            int? onlineStoreId,
            string category,
            int? conditionId,
            string searchstring,
            double? usdMin,
            double? usdMax)
        {
            return _mapper.Map<List<ListingViewModel>>(await _listingLogic.GetAsync(onlineStoreId, category, conditionId, searchstring, usdMin, usdMax));
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetListing")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<OnlineStoreViewModel>> GetSingle(int id)
        {
            var listing = await _listingLogic.GetById(id);
            if (listing == null)
            {
                return NotFound();
            }
            return _mapper.Map<OnlineStoreViewModel>(listing);
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ListingViewModel>> Post([FromBody] ListingViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var listing = _mapper.Map<Listing>(viewModel);
            listing.ItemMetaData.ItemMetaDataCategories = viewModel.ItemMetaData.CategoryIds.ToList().Select(id =>
            new ItemMetaDataCategory()
                {
                    CategoryId = id
                }).ToList();
            await _listingLogic.CreateAsync(null, listing);
            return CreatedAtRoute("GetListing", new { id = listing.Id }, _mapper.Map<ListingViewModel>(listing));
        }

    }
}
