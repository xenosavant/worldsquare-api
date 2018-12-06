using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Services.Interfaces
{
    public interface ISecurityService
    {
        Task<bool> IsAllowedToPostListingReview(int userId, int listingId);
        Task<bool> IsAllowedToViewReviewsForService(int userId, int serviceId);
    }
}
