using AutoMapper;
using Bounce.Api.Data.Search.Indexes;
using Stellmart.Api.Business.Extensions;
using Stellmart.Api.Business.Logic.Interfaces;
using Stellmart.Api.Business.Managers.Interfaces;
using Stellmart.Api.Context.Entities;
using Stellmart.Api.Data;
using Stellmart.Api.Data.Search.Queries;
using Stellmart.Api.Data.ViewModels;
using Stellmart.Api.DataAccess;
using Stellmart.Api.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Logic
{
    public class ListingLogic : IListingLogic
    {
        private readonly ISearchService _searchService;
        private readonly IListingDataManager _manager;
        private readonly IInventoryDataManager _inventoryManager;
        private readonly IItemMetaDataManager _metaDataManager;

        public ListingLogic(IMapper mapper,
            IListingDataManager manager,
            ISearchService searchService,
            IInventoryDataManager inventoryManager,
            IItemMetaDataManager metaDataManager)
        {
            _manager = manager;
            _searchService = searchService;
            _metaDataManager = metaDataManager;
            _inventoryManager = inventoryManager;
        }

        public async Task<IEnumerable<Listing>> GetAsync(int? onlineStroreId,  string category,
            int? conditionId, string searchString, double? usdMin,
            double? usdMax)
        {
            var query = new ItemSearchQuery()
            {
                Category = category,
                ParentId = onlineStroreId,
                ItemConditionId = conditionId,
                MinimumPriceUsd = usdMin,
                MaximumPriceUsd = usdMax
            };
            var ids = (await _searchService.SearchAsync<Listing, ItemSearchQuery>(searchString, query)).ToList();
            return await _manager.GetAsync(ids);
        } 

        public Task<Listing> GetById(int id)
        {
            return _manager.GetById(id);
        }

        public async Task<Listing> CreateAsync(int? userId, Listing listing)
        {
            await _metaDataManager.UpdateRelationshipsAsync(listing.ItemMetaData);
            _inventoryManager.Create(listing.InventoryItems);
            var newListing = await _manager.CreateAsync(listing, null);
            await _searchService.IndexAsync<Listing, ItemMetaDataSearchIndex>(
                    new List<ItemMetaDataSearchIndex>() { new ItemMetaDataSearchIndex(listing) });
            return newListing;
        }

        public async Task<Listing> UpdateAsync(Listing listing, Delta<Listing> delta)
        {
            delta.Patch(listing);
            var updatedListing = await _manager.UpdateAsync(listing);
            await _searchService.UpdateAsync<Listing, ItemMetaDataSearchIndex>(
                    new List<ItemMetaDataSearchIndex>() { new ItemMetaDataSearchIndex(updatedListing) });
            return updatedListing;
        }

        public Task Delete(Listing store)
        {
            return _manager.Delete(store);
        }
    }
}
