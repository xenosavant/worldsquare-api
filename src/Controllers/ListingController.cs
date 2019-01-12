using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Stellmart.Api.Business.Logic.Interfaces;
using Stellmart.Api.Business.Managers.Interfaces;
using Stellmart.Api.Context.Entities;
using Stellmart.Api.Data;
using Stellmart.Api.Data.Listing;
using Stellmart.Api.Data.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stellmart.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ListingController : BaseController
    {
        private readonly int UserId = 1;
        private readonly IListingLogic _listingLogic;
        private readonly IListingDataManager _listingDataManager;
        private readonly IMapper _mapper;

        public ListingController(
            IListingLogic listingLogic, 
            IMapper mapper, 
            IListingDataManager listingDataManager)
        {
            _listingLogic = listingLogic;
            _listingDataManager = listingDataManager;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<ListingSearchResponse>> Get(
            //[ModelBinder(
            //typeof(CommaDelimitedArrayModelBinder))]
            [FromQuery]
            int? onlineStoreId,
            string category,
            int? conditionId,
            string searchstring,
            double? usdMin,
            double? usdMax,
            double? xlmMin,
            double? xlmMax,
            int page)
        {
            var pageLength = 20;

            var result = await _listingLogic.GetAsync(onlineStoreId, category, 
                conditionId, searchstring, usdMin, usdMax, xlmMin, xlmMax, page, pageLength);
            return new ListingSearchResponse()
            {
                Listings = _mapper.Map<List<ListingViewModel>>(result.Listings),
                Count = result.Count
            };
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetListing")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ListingViewModel>> GetSingle(int id)
        {
            var listing = await _listingDataManager.GetById(id);
            if (listing == null)
            {
                return NotFound();
            }
            return _mapper.Map<ListingViewModel>(listing);
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
            var newListing = await _listingLogic.CreateAsync(1, listing);
            return CreatedAtRoute("GetListing", new { id = newListing.Id }, _mapper.Map<ListingViewModel>(newListing));
        }

        [HttpPatch]
        [Route("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ListingViewModel>> Patch(int id, [FromBody] Delta<Listing> delta)
        {
            var listing = await _listingDataManager.GetById(id, "OnlineStore,ItemMetaData");
            if (delta.ContainsKey("UniqueId"))
            {
                return BadRequest();
            }
            if (listing.OnlineStore.UserId != UserId)
            {
                return Unauthorized();
            }
            return _mapper.Map<ListingViewModel>(await _listingLogic.UpdateAsync(UserId, listing, delta));
        }

        [HttpDelete]
        [Route("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Delete(int id)
        {
            var listing = await _listingDataManager.GetById(id, "OnlineStore,ItemMetaData");
            if (listing.OnlineStore.UserId != UserId)
            {
                return Unauthorized();
            }
            await _listingLogic.DeleteAsync(listing);
            return NoContent();
        }

    }
}
