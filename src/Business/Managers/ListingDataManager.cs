using Stellmart.Api.Business.Managers.Interfaces;
using Stellmart.Api.Context.Entities;
using Stellmart.Api.Context.Entities.ReadOnly;
using Stellmart.Api.DataAccess;
using Stellmart.Api.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Managers
{
    public class ListingDataManager : IListingDataManager
    {
        private readonly IRepository _repository;

        public static string NavigationProperties => "InventoryItems.Price," +
            "UnitType,ItemMetaData.ItemMetaDataCategories.Category";

        public ListingDataManager(IRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Listing>> GetAsync(List<int> ids = null)
        {
            return _repository.GetAsync<Listing>(l => ids.Contains(l.Id), null, NavigationProperties);
        }

        public Task<Listing> GetByMetaDataId(int metaDataId, string navigationProperties = null)
        {
            return _repository.GetOneAsync<Listing>(l => l.ItemMetaDataId == metaDataId, navigationProperties ?? NavigationProperties);
        }

        public Task<Listing> GetById(int id, string navigationProperties = null)
        {
            return _repository.GetOneAsync<Listing>(s => s.Id == id, navigationProperties ?? NavigationProperties);
        }

        public Listing Create(Listing listing, int userId)
        {
            _repository.Create(listing, userId);
            return listing;
        }

        public async Task<Listing> CreateAndSaveAsync(Listing listing, int userId)
        {
            _repository.Create(listing, userId);
            await _repository.SaveAsync();
            return listing;
        }

        public Listing Update(Listing listing, int userId)
        {
            _repository.Update(listing);
            return listing;
        }

        public async Task<Listing> UpdateAndSaveAsync(Listing listing, int userId)
        {
            _repository.Update(listing);
            await _repository.SaveAsync();
            return listing;
        }

        public Task Delete(Listing store)
        {
            store.IsDeleted = true;
            _repository.Update(store);
            return _repository.SaveAsync();
        }
    }
}
