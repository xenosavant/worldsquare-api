using Stellmart.Api.Context.Entities;
using Stellmart.Api.Data;
using Stellmart.Api.Data.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Logic
{
    public interface IOnlineStoreLogic
    {
        Task<IEnumerable<OnlineStore>> GetAll();
        Task<OnlineStore> GetById(int id);
        Task<OnlineStore> Create(int userId, OnlineStoreViewModel store);
        Task<OnlineStore> Update(int userId, OnlineStore store, Delta<OnlineStore> delta);
        Task Delete(OnlineStore store);
    }
}
