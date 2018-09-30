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
        Task<Listing> GetById(int id, string navigationProperties = null);
        Task<Listing> GetByMetaDataId(int metaDataId, string navigationProperties = null);
        Listing Create(Listing listing, int userId);
        Task<Listing> CreateAndSaveAsync(Listing listing, int userId);
        Listing Update(Listing listing, int userId);
        Task<Listing> UpdateAndSaveAsync(Listing listing, int userId);
        Task Delete(Listing store);
    }
}
