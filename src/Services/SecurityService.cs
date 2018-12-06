using Microsoft.EntityFrameworkCore;
using Stellmart.Api.Context.Entities;
using Stellmart.Api.DataAccess;
using Stellmart.Api.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly IRepository _repository;

        public SecurityService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> IsAllowedToPostListingReview(int userId, int listingId)
        {
            if (_repository.GetQueryable<Review>().Any(r => r.ListingId == listingId && r.CreatedBy == userId))
            {
                return false;
            }
            var queryable = _repository.GetQueryable<Listing>(l => l.InventoryItems.Any(i => i.OrderItems.Select(oi => oi.Order.PurchaserId == userId).Any()));
            return await queryable.AnyAsync();
        }

        public Task<bool> IsAllowedToViewReviewsForService(int userId, int serviceId)
        {
            return _repository.GetQueryable<OnlineStore>(s => s.UserId == userId && s.Id == serviceId).AnyAsync();
        }

        public async Task<bool> IsAllowedToPostListingThread(int userId, int listingId)
        {
            if (_repository.GetQueryable<MessageThread>().Any(r => r.ListingId == listingId && r.CreatedBy == userId))
            {
                return false;
            }
            var queryable = _repository.GetQueryable<Listing>(l => l.InventoryItems.Any(i => i.OrderItems.Select(oi => oi.Order.PurchaserId == userId).Any()));
            return await queryable.AnyAsync();
        }
    }
}
