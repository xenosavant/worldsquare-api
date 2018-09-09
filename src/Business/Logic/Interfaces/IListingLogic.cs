using Stellmart.Api.Context.Entities;
using Stellmart.Api.Data;
using Stellmart.Api.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Logic.Interfaces
{
    public interface IListingLogic
    {
        Task<IEnumerable<Listing>> GetAsync(string [] keywords, int? onlineStoreId, 
            int? categoryId, int? subcategoryId, int? listingCategoryId, 
            int? conditionId, string searchString);
        Task<Listing> GetByIdAsync(int id);
        Task<Listing> CreateAsync(int userId, ListingViewModel listing);
        Task<Listing> UpdateAsync(Listing listing, Delta<Listing> delta);
        Task<Listing> UpdateAsync(Listing listing);
        Task DeleteAsync(Listing store);
    }
}
