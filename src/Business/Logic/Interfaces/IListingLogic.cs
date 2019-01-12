using Stellmart.Api.Context.Entities;
using Stellmart.Api.Data;
using Stellmart.Api.Data.Listing;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Logic.Interfaces
{
    public interface IListingLogic
    {
        Task<ListingSearchDto> GetAsync(int? onlineStroreId, string category,
            int? conditionId, string searchString, double? usdMin,
            double? usdMax, double? xlmMin, double? xlmMax, int page, int pageLength);
        Task<Listing> GetById(int id);
        Task<Listing> CreateAsync(int userId, Listing listing);
        Task<Listing> UpdateAsync(int userId, Listing listing, Delta<Listing> delta);
        Task DeleteAsync(Listing store);
    }
}
