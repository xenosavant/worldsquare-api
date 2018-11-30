using Stellmart.Api.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Managers.Interfaces
{
    public interface ICartDataManager
    {
        Task<Cart> CreateAsync(InventoryItem item, int userId);
        Task<Cart> GetAsync(int userId, string properties = null);
        Task<Cart> SaveAsync(Cart cart);
    }
}
