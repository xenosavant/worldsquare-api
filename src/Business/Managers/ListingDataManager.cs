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
        private readonly ISearchService _searchService;

        public static string NavigationProperties => "InventoryItems.Price," +
            "InventoryItems.UnitType,Thread";

        public ListingDataManager(IRepository repository, ISearchService searchService)
        {
            _repository = repository;
            _searchService = searchService;
        }


        public Task<IEnumerable<Listing>> GetAsync(List<int> ids = null)
        {
            return _repository.GetAsync<Listing>(l => ids.Contains(l.Id));
        }

        public Task<Listing> GetById(int id)
        {
            return _repository.GetOneAsync<Listing>(s => s.Id == id, NavigationProperties);
        }

        public async Task<Listing> CreateAsync(Listing listing, int? userId = null)
        {
            var ids = listing.ItemMetaData.ItemMetaDataCategories.Select(l => l.CategoryId).ToList();
            var categories = await _repository.GetAsync<Category>(c => ids.Any(id => id == c.Id));
            listing.ItemMetaData.ItemMetaDataCategories =
                listing.ItemMetaData.ItemMetaDataCategories.Select(lc =>
               new ItemMetaDataCategory()
               {
                   Category = categories.Where(c => c.Id == lc.CategoryId).Single(),
                   ItemMetaData = listing.ItemMetaData
               }).ToList();
            _repository.Create(listing, userId);
            await _repository.SaveAsync();
            return await _repository.GetOneAsync<Listing>(l => l.Id == listing.Id, NavigationProperties);
        }

        public async Task<Listing> UpdateAsync(Listing listing)
        {
            _repository.Update(listing);
            await _repository.SaveAsync();
            return await _repository.GetOneAsync<Listing>(o => o.Id == listing.Id, NavigationProperties);
        }

        public Task Delete(Listing store)
        {
            store.IsDeleted = true;
            _repository.Update(store);
            return _repository.SaveAsync();
        }
    }
}
