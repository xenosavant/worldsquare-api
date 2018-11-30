using Stellmart.Api.Context.Entities;
using Stellmart.Api.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Logic.Interfaces
{
    public interface IListingLogic
    {
        Task<IEnumerable<Listing>> GetAsync(int? onlineStoreId,
            string category, int? conditionId, string searchString,
            double? usdMin, double? usdMax, int? page, int? pageLength);
        Task<Listing> GetById(int id);
        Task<Listing> CreateAsync(int userId, Listing listing);
        Task<Listing> UpdateAsync(int userId, Listing listing, Delta<Listing> delta);
        Task DeleteAsync(Listing store);
    }
}
