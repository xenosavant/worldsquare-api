using AutoMapper;
using Bounce.Api.Data.Search.Indexes;
using Stellmart.Api.Business.Logic.Interfaces;
using Stellmart.Api.Business.Managers.Interfaces;
using Stellmart.Api.Context.Entities;
using Stellmart.Api.Data;
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

        public async Task<IEnumerable<Listing>> GetAsync(int? onlineStroreId,  string category,
            int? conditionId, string searchString, double? usdMin,
            double? usdMax, int? page, int? pageLength)
        {
            var query = new ItemSearchQuery()
            {
                Category = category,
                ParentId = onlineStroreId,
                ItemConditionId = conditionId,
                MinimumPriceUsd = usdMin,
                MaximumPriceUsd = usdMax
            };
            var ids = (await _searchService.SearchAsync<Listing, ItemSearchQuery>(searchString + "~1", query)).ToList();
            if (page != null && pageLength != null)
            {
                ids = ids.Skip(((int)page - 1) * (int)pageLength).Take((int)pageLength).ToList();
            }
            return await _listingManager.GetAsync(ids);
        } 

        public Task<Listing> GetById(int id)
        {
            return _listingManager.GetById(id);
        }

        public async Task<Listing> CreateAsync(int userId, Listing listing)
        {
            _inventoryManager.Create(listing.InventoryItems);
            await _metaDataManager.UpdateRelationshipsAsync(listing.ItemMetaData);
            listing.IsActive = true;
            var newListing = await _listingManager.CreateAndSaveAsync(listing, userId);
            await _searchService.IndexAsync<Listing, ItemMetaDataSearchIndex>(
                    new List<ItemMetaDataSearchIndex>() { new ItemMetaDataSearchIndex(listing) });
            return await _listingManager.GetById(newListing.Id);
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
