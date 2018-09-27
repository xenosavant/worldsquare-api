using Stellmart.Api.Context.Entities;
using Stellmart.Api.Data;
using Stellmart.Api.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Managers.Interfaces
{
    public interface IOnlineStoreDataManager
    {
        Task<IEnumerable<OnlineStore>> GetAll();
        Task<OnlineStore> GetById(int id);
        Task<OnlineStore> CreateAsync(OnlineStore store);
        Task<OnlineStore> UpdateAsync(OnlineStore store);
        Task Delete(OnlineStore store);
    }
}
