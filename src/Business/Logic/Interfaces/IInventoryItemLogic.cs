using Stellmart.Api.Context.Entities;
using Stellmart.Api.Data;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Logic.Interfaces
{
    public interface IInventoryItemLogic
    {
        Task<InventoryItem> GetById(int id, string navigationProperties = null);
        Task<InventoryItem> CreateAndSaveAsync(int userId, InventoryItem item);
        Task<InventoryItem> UpdateAndSaveAsync(int userId, InventoryItem item, Delta<InventoryItem> delta);
        Task DeleteAsync(InventoryItem item);
    }
}
