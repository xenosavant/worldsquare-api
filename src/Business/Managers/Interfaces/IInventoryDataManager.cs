using Stellmart.Api.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Managers.Interfaces
{
    public interface IInventoryDataManager
    {
        Task<IEnumerable<InventoryItem>> GetAsync();
        Task<InventoryItem> GetById(int id);
        ICollection<InventoryItem> Create(ICollection<InventoryItem> items, int? userId = null);
    }
}
