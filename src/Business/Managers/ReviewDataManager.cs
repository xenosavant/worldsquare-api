using Stellmart.Api.Business.Managers.Interfaces;
using Stellmart.Api.Context.Entities;
using Stellmart.Api.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Managers
{
    public class ReviewDataManager : IReviewDataManager
    {
        private readonly IRepository _repository;

        public ReviewDataManager(IRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Review>> GetReviewsForListingAsync(int listingId)
        {
            return _repository.GetAsync<Review>(r => r.ListingId == listingId);
        }

        public Task<IEnumerable<Review>> GetReviewsForServiceAsync(int serviceId)
        {
            return _repository.GetAsync<Review>(r => r.ServiceId == serviceId, null, "Service");
        }

        public Task<Review> GetById(int id)
        {
            return _repository.GetOneAsync<Review>(s => s.Id == id, "Service");
        }

        public async Task<Review> CreateAndSaveAsync(Review review, int userId)
        {
            _repository.Create(review, userId);
            await _repository.SaveAsync();
            return await GetById(review.Id);
        }
    }
}
