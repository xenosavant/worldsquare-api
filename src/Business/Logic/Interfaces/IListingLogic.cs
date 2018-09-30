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
        Task<IEnumerable<Listing>> GetAsync(int? onlineStoreId,
            string category, int? conditionId, string searchString,
            double? usdMin, double? usdMax);
        Task<Listing> GetById(int id);
        Task<Listing> CreateAsync(int userId, Listing listing);
        Task<Listing> UpdateAsync(int userId, Listing listing, Delta<Listing> delta);
        Task DeleteAsync(Listing store);
    }
}
