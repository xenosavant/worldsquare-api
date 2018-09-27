using Stellmart.Api.Business.Managers.Interfaces;
using Stellmart.Api.Context.Entities;
using Stellmart.Api.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Managers
{
    public class InventoryItemDataManager : IInventoryDataManager
    {
        private readonly IRepository _repository;

        public static string NavigationProperties => "Price,UnitType,Thread";

        public InventoryItemDataManager(IRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<InventoryItem>> GetAsync()
        {
            return _repository.GetAsync<InventoryItem>();
        }

        public Task<InventoryItem> GetById(int id)
        {
            return _repository.GetOneAsync<InventoryItem>(s => s.Id == id, NavigationProperties);
        }

        public ICollection<InventoryItem> Create(ICollection<InventoryItem> items, int? userId = null)
        {
            _repository.CreateRange(items, (int)userId);
            return items;
        }
    }
}
