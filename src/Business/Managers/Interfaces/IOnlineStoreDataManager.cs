using Stellmart.Api.Context.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Managers.Interfaces
{
    public interface IOnlineStoreDataManager
    {
        Task<IEnumerable<OnlineStore>> GetAll();
        Task<OnlineStore> GetById(int id);
        OnlineStore Create(OnlineStore store, int id);
        Task<OnlineStore> CreateAndSaveAsync(OnlineStore store, int id);
        OnlineStore Update(OnlineStore store, int id);
        Task<OnlineStore> UpdateAndSaveAsync(OnlineStore store, int id);
        Task Delete(OnlineStore store);
    }
}
