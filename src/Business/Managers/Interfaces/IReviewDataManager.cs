using Stellmart.Api.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Managers.Interfaces
{
    public interface IReviewDataManager
    {
        Task<IEnumerable<Review>> GetReviewsForListingAsync(int listingId);
        Task<IEnumerable<Review>> GetReviewsForServiceAsync(int serviceId);
        Task<Review> GetById(int id);
        Task<Review> CreateAndSaveAsync(Review review, int userId);
    }
}
