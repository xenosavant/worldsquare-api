using Stellmart.Api.Business.Managers.Interfaces;
using Stellmart.Api.Context.Entities;
using Stellmart.Api.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Managers
{
    public class InventoryItemDataManager : IInventoryItemDataManager
    {
        private readonly IRepository _repository;

        public static string NavigationProperties => "Price";

        public InventoryItemDataManager(IRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<InventoryItem>> GetAsync()
        {
            return _repository.GetAsync<InventoryItem>();
        }

        public Task<InventoryItem> GetById(int id, string navigationProperties = null)
        {
            return _repository.GetOneAsync<InventoryItem>(s => s.Id == id, navigationProperties ?? NavigationProperties);
        }

        public ICollection<InventoryItem> Create(ICollection<InventoryItem> items)
        {
            _repository.CreateRange(items);
            return items;
        }

        public async Task<ICollection<InventoryItem>> CreateAndSaveAsync(ICollection<InventoryItem> items)
        {
            _repository.CreateRange(items);
            await _repository.SaveAsync();
            return items;
        }

        public ICollection<InventoryItem> Update(ICollection<InventoryItem> items)
        {
            foreach (var item in items)
            {
                _repository.Update(item);
            }
            return items;
        }

        public async Task<ICollection<InventoryItem>>  UpdateAndSaveAsync(ICollection<InventoryItem> items)
        {
            foreach (var item in items)
            {
                _repository.Update(item);
            }
            await _repository.SaveAsync();
            return items;
        }

        public Task Delete(InventoryItem item)
        {
            _repository.Delete(item);
            return _repository.SaveAsync();
        }
    }
}
