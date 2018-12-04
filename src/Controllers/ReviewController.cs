using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Stellmart.Api.Business.Logic.Interfaces;
using Stellmart.Api.Business.Managers.Interfaces;
using Stellmart.Api.Context.Entities;
using Stellmart.Api.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ReviewController : AuthorizedController
    {
        private readonly IMapper _mapper;
        private readonly IReviewLogic _reviewLogic;
        private readonly IReviewDataManager _reviewManager;

        public ReviewController(
          IMapper mapper,
          IReviewLogic reviewLogic,
          IReviewDataManager reviewManager)
        {
            _mapper = mapper;
            _reviewLogic = reviewLogic;
            _reviewManager = reviewManager;
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
        public async Task<ActionResult<Review>> Post([FromBody] ReviewViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var review = _mapper.Map<Review>(viewModel);
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
