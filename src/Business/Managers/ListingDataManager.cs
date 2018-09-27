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
            "InventoryItems.UnitType,Thread";

        public ListingDataManager(IRepository repository)
        {
            _repository = repository;
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
