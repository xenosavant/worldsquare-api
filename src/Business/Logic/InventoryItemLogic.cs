using Bounce.Api.Data.Search.Indexes;
using Stellmart.Api.Business.Logic.Interfaces;
using Stellmart.Api.Business.Managers.Interfaces;
using Stellmart.Api.Context.Entities;
using Stellmart.Api.Data;
using Stellmart.Api.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Logic
{
    public class InventoryItemLogic : IInventoryItemLogic
    {
        private readonly ISearchService _searchService;
        private readonly IInventoryItemDataManager _inventoryManager;
        private readonly IListingDataManager _listingManager;

        public InventoryItemLogic(
            ISearchService searchService,
            IInventoryItemDataManager inventoryManager,
            IListingDataManager listingManager)
        {
            _searchService = searchService;
            _inventoryManager = inventoryManager;
            _listingManager = listingManager;
        }

        public Task<InventoryItem> GetById(int id, string navigationProperties = null)
        {
            return _inventoryManager.GetById(id, navigationProperties);
        }

        public async Task<InventoryItem> CreateAndSaveAsync(int userId, InventoryItem item)
        {
            await _inventoryManager.CreateAndSaveAsync(new List<InventoryItem>() { item });
            var listing = await _listingManager.GetById((int)item.ListingId,
                "InventoryItems.Price,ItemMetaData,ItemMetaData.ItemMetaDataCategories.Category");
            await _searchService.UpdateAsync<Listing, ItemMetaDataSearchIndex>(
                new List<ItemMetaDataSearchIndex>() { new ItemMetaDataSearchIndex(listing) });
            return item;
        }

        public async Task<InventoryItem> UpdateAndSaveAsync(int userId, InventoryItem item, Delta<InventoryItem> delta)
        {
            if (delta.ContainsKey("CurrencyAmount"))
            {
                item.Price.Amount = delta["CurrencyAmount"];
                delta.Remove("CurrencyAmount");
            }
            if (delta.ContainsKey("CurrencyTypeId"))
            {
                item.Price.CurrencyTypeId = delta["CurrencyTypeId"];
                delta.Remove("CurrencyTypeId");
            }
            delta.Patch(item);
            var savedItem = await _inventoryManager.UpdateAndSaveAsync(new List<InventoryItem>() { item });
            var listing = await _listingManager.GetById((int)item.ListingId,
               "InventoryItems.Price,ItemMetaData,ItemMetaData.ItemMetaDataCategories.Category");
            await _searchService.UpdateAsync<Listing, ItemMetaDataSearchIndex>(
                new List<ItemMetaDataSearchIndex>() { new ItemMetaDataSearchIndex(listing) });
            return savedItem.FirstOrDefault();
        }

        public Task DeleteAsync(InventoryItem item)
        {
            return _inventoryManager.Delete(item);
        }
    }
}
