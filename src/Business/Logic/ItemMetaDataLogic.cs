using Bounce.Api.Data.Search.Indexes;
using Newtonsoft.Json;
using Stellmart.Api.Business.Logic.Interfaces;
using Stellmart.Api.Business.Managers.Interfaces;
using Stellmart.Api.Context.Entities;
using Stellmart.Api.Data;
using Stellmart.Api.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Logic
{
    public class ItemMetaDataLogic : IItemMetaDataLogic
    {
        private readonly ISearchService _searchService;
        private readonly IItemMetaDataManager _metaDataManager;
        private readonly IListingDataManager _listingManager;

        public ItemMetaDataLogic(
            ISearchService searchService,
            IItemMetaDataManager metaDataManager,
            IListingDataManager listingManager)
        {
            _searchService = searchService;
            _metaDataManager = metaDataManager;
            _listingManager = listingManager;
        }

        public Task<ItemMetaData> GetById(int id, string navigationProperties = null)
        {
            return _metaDataManager.GetById(id, navigationProperties);
        }

        public async Task<ItemMetaData> UpdateAndSaveAsync(int userId, ItemMetaData metaData, Delta<ItemMetaData> delta)
        {
            if (delta.ContainsKey("KeyWords"))
            {
                metaData.KeyWords = JsonConvert.SerializeObject(delta["KeyWords"]);
                delta.Remove("KeyWords");
            }
            delta.Patch(metaData);
            var savedMetaData = await _metaDataManager.UpdateAndSaveAsync(metaData);
            var listing = await _listingManager.GetByMetaDataId(savedMetaData.Id);
            await _searchService.UpdateAsync<Listing, ItemMetaDataSearchIndex>(
               new List<ItemMetaDataSearchIndex>() { new ItemMetaDataSearchIndex(listing) });
            return savedMetaData;
        }
    }
}
