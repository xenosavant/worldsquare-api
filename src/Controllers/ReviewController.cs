using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Stellmart.Api.Business.Logic.Interfaces;
using Stellmart.Api.Business.Managers.Interfaces;
using Stellmart.Api.Context.Entities;
using Stellmart.Api.Data.ViewModels;
using Stellmart.Api.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ReviewController : BaseController
    {
        private readonly int UserId = 1;
        private readonly IMapper _mapper;
        private readonly IReviewLogic _reviewLogic;
        private readonly IReviewDataManager _reviewManager;
        private readonly IListingDataManager _listingManager;
        private readonly ISecurityService _securityService;

        public ReviewController(
          IMapper mapper,
          IReviewLogic reviewLogic,
          IReviewDataManager reviewManager,
          IListingDataManager listingManager,
          ISecurityService securityService)
        {
            _mapper = mapper;
            _reviewLogic = reviewLogic;
            _reviewManager = reviewManager;
            _listingManager = listingManager;
            _securityService = securityService;
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<ReviewViewModel>>> Get([FromQuery]
            int? serviceId,
            int? listingId,
            int? page,
            int? pageLength
        )
        {
            if (serviceId == null && listingId == null)
            {
                return BadRequest();
            }
            if (serviceId != null && 
                !(await _securityService.IsAllowedToViewReviewsForService((int)serviceId, UserId)))
            {
                return Unauthorized();
            }
            return GetViewModels((await _reviewLogic.GetAsync(serviceId, listingId, page, pageLength)).ToList());
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetReview")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ReviewViewModel>> GetSingle(int id)
        {
            var review = await _reviewManager.GetById(id);
            if (review == null)
            {
                return NotFound();
            }
            var reviews = GetViewModels(new List<Review>() { review });
            return reviews.FirstOrDefault();
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ReviewViewModel>> Post([FromBody] CreateReviewRequest request)
        {
            if (!ModelState.IsValid || request.Stars > 5 || request.Stars < 0 ||
                (request.ServiceId == null && request.ListingId == null))
            {
                return BadRequest();
            }
            var review = _mapper.Map<Review>(request);
            if (request.ServiceId == null)
            { 
                var listing = await _listingManager.GetById((int)request.ListingId, "OnlineStore,InventoryItems");
                if (!(await _securityService.IsAllowedToPostListingReview(UserId, listing.Id)))
                {
                    return Unauthorized();
                }
                review.ServiceId = listing.OnlineStore.Id;
            }
            var newReview = await _reviewManager.CreateAndSaveAsync(review, UserId);
            var reviews = GetViewModels(new List<Review>() { newReview });
            return CreatedAtRoute("GetReview", new { id = newReview.Id }, reviews.FirstOrDefault());
        }

        private List<ReviewViewModel> GetViewModels(List<Review> reviews)
        {
            switch (reviews.First().Service.GetType().Name)
            {
                case "OnlineStore":
                    return _mapper.Map<List<ReviewViewModel>>(reviews);
                default:
                    return new List<ReviewViewModel>();
            }
        }
    }
}
