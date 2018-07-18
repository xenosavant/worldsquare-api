using Microsoft.AspNetCore.Mvc;
using Stellmart.Api.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stellmart.Api.Data.ViewModels;

namespace Stellmart.Api.Business.Logic
{
    public interface IOnlineStoreLogic
    {
        Task<IEnumerable<OnlineStore>> GetAllAsync();
        Task<OnlineStore> GetByIdAsync(int id);
        Task<OnlineStore> CreateAsync(int userId, OnlineStoreViewModel store);
        Task<OnlineStore> UpdateAsync(OnlineStoreViewModel vm, OnlineStore store);
        Task DeleteAsync(OnlineStore store);
    }
}
