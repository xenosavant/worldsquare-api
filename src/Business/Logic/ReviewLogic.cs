using Stellmart.Api.Business.Logic.Interfaces;
using Stellmart.Api.Business.Managers.Interfaces;
using Stellmart.Api.Context.Entities;
using Stellmart.Api.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Logic
{
    public class ReviewLogic : IReviewLogic
    {
        private readonly IReviewDataManager _reviewManager;
        private readonly IListingDataManager _listingManager;

        public ReviewLogic(IReviewDataManager reviewManager)
        {
            _reviewManager = reviewManager;
        }

        public async Task<IEnumerable<Review>> GetAsync(
            int? serviceId,
            int? listingId,
            int? page,
            int? pageLength)
        {
            if (listingId != null)
            {
                return await _reviewManager.GetReviewsForListingAsync((int)listingId);
            }
            else
            {
                return await _reviewManager.GetReviewsForServiceAsync((int)serviceId);
            }
        }

        public async Task<IEnumerable<Review>> CreateAsync(Review review)
        {
            //if (review.ServiceId == null)
            //{
            //    var review = _reviewManager.GetById(review)
            //}
            throw new NotImplementedException();
        }

    }
}
