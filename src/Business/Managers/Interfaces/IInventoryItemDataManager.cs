using Stellmart.Api.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Managers.Interfaces
{
    public interface IInventoryItemDataManager
    {
        Task<IEnumerable<InventoryItem>> GetAsync();
        Task<InventoryItem> GetById(int id, string navigationProperties = null);
        ICollection<InventoryItem> Create(ICollection<InventoryItem> items);
        Task<ICollection<InventoryItem>> CreateAndSaveAsync(ICollection<InventoryItem> items);
        ICollection<InventoryItem> Update(ICollection<InventoryItem> items);
        Task<ICollection<InventoryItem>> UpdateAndSaveAsync(ICollection<InventoryItem> items);
        Task Delete(InventoryItem item);
    }
}
