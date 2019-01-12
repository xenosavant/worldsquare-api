using AutoMapper;
using Bounce.Api.Data.Search.Indexes;
using Stellmart.Api.Business.Logic.Interfaces;
using Stellmart.Api.Business.Managers.Interfaces;
using Stellmart.Api.Context.Entities;
using Stellmart.Api.Data;
using Stellmart.Api.Data.Listing;
using Stellmart.Api.Data.Search.Queries;
using Stellmart.Api.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Logic
{
    public class ListingLogic : IListingLogic
    {
        private readonly ISearchService _searchService;
        private readonly IListingDataManager _listingManager;
        private readonly IInventoryItemDataManager _inventoryManager;
        private readonly IItemMetaDataManager _metaDataManager;

        public ListingLogic(IMapper mapper,
            IListingDataManager listingManager,
            ISearchService searchService,
            IInventoryItemDataManager inventoryManager,
            IItemMetaDataManager metaDataManager)
        {
            _listingManager = listingManager;
            _searchService = searchService;
            _metaDataManager = metaDataManager;
            _inventoryManager = inventoryManager;
        }

        public async Task<ListingSearchDto> GetAsync(int? onlineStroreId,  string category,
            int? conditionId, string searchString, double? usdMin,
            double? usdMax, double? xlmMin, double? xlmMax, int page, int pageLength)
        {
            var query = new ItemSearchQuery()
            {
                Category = category,
                ParentId = onlineStroreId,
                ItemConditionId = conditionId,
                MinimumPriceUsd = usdMin,
                MaximumPriceUsd = usdMax
            };
            var searchResult = await _searchService.SearchAsync<Listing, ItemSearchQuery>(searchString, query, (page - 1) * pageLength, pageLength);
            var results = (await _listingManager.GetAsync(searchResult.Ids)).ToList();
            return new ListingSearchDto()
            {
                Listings = results,
                Count = results.Count
            };
        } 

        public Task<Listing> GetById(int id)
        {
            return _listingManager.GetById(id);
        }

        public async Task<Listing> CreateAsync(int userId, Listing listing)
        {
            foreach (var item in listing.InventoryItems)
            {
                item.Price = item.Price * listing.Currency.Precision;
            }
            _inventoryManager.Create(listing.InventoryItems);
            await _metaDataManager.UpdateRelationshipsAsync(listing.ItemMetaData);
            listing.IsActive = true;
            listing.CurrencyId = listing.Currency.Id;
            listing.Currency = null;
            var newListing = await _listingManager.CreateAndSaveAsync(listing, userId);
            newListing =  await _listingManager.GetById(newListing.Id);
            await _searchService.IndexAsync<Listing, ItemMetaDataSearchIndex>(
                   new List<ItemMetaDataSearchIndex>() { new ItemMetaDataSearchIndex(newListing) });
            return newListing;
        }

        public async Task<Listing> UpdateAsync(int userId, Listing listing, Delta<Listing> delta)
        {
            delta.Patch(listing);
            var updatedListing = await _listingManager.UpdateAndSaveAsync(listing, userId);
            await _searchService.UpdateAsync<Listing, ItemMetaDataSearchIndex>(
                    new List<ItemMetaDataSearchIndex>() { new ItemMetaDataSearchIndex(updatedListing) });
            return updatedListing;
        }

        public async Task DeleteAsync(Listing listing)
        {
            await _listingManager.Delete(listing);
            await _searchService.DeleteAsync<Listing, ItemMetaDataSearchIndex>(
                new List<ItemMetaDataSearchIndex>() { new ItemMetaDataSearchIndex(listing) });
        }
    }
}
