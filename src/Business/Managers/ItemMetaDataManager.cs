﻿using Stellmart.Api.Business.Managers.Interfaces;
using Stellmart.Api.Context.Entities;
using Stellmart.Api.Context.Entities.ReadOnly;
using Stellmart.Api.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Managers
{
    public class ItemMetaDataManager : IItemMetaDataManager
    {
        private readonly IRepository _repository;
        private readonly ILookupDataManager _lookupManager;

        public ItemMetaDataManager(IRepository repository, ILookupDataManager lookupManager)
        {
            _lookupManager = lookupManager;
            _repository = repository;
        }

        public Task<ItemMetaData> GetById(int id, string navigationProperties = null)
        {
            return _repository.GetOneAsync<ItemMetaData>(s => s.Id == id, navigationProperties);
        }

        public async Task<ItemMetaData> UpdateAndSaveAsync(ItemMetaData metaData)
        {
            _repository.Update(metaData);
            await _repository.SaveAsync();
            return metaData;
        }

        public async Task UpdateRelationshipsAsync(ItemMetaData metaData, int? userId = null)
        {
            var ids = metaData.ItemMetaDataCategories.Select(l => l.CategoryId).ToList();
            var categories = await _lookupManager.GetAsync<Category>(ids);
            metaData.ItemMetaDataCategories =
                metaData.ItemMetaDataCategories.Select(lc =>
               new ItemMetaDataCategory()
               {
                   Category = categories.Where(c => c.Id == lc.CategoryId).Single(),
                   ItemMetaData = metaData
               }).ToList();
        }
    }
}
