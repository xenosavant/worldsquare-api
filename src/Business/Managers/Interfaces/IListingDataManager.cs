using Stellmart.Api.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Managers.Interfaces
{
    public interface IListingDataManager
    {
        Task<IEnumerable<Listing>> GetAsync(List<int> ids = null);
        Task<Listing> GetById(int id);
        Task<Listing> CreateAsync(Listing listing, int? userId);
        Task<Listing> UpdateAsync(Listing listing);
        Task Delete(Listing store);
    }
}
