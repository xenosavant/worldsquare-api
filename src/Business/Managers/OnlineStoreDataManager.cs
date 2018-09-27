using Stellmart.Api.Business.Managers.Interfaces;
using Stellmart.Api.Context.Entities;
using Stellmart.Api.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Managers
{
    public class OnlineStoreDataManager : IOnlineStoreDataManager
    {
        private readonly IRepository _repository;
        public static string NavigationProperties => "User,NativeCurrency,ServiceRegion";

        public OnlineStoreDataManager(IRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<OnlineStore>> GetAll()
        {
            return  _repository.GetAllAsync<OnlineStore>();
        }

        public Task<OnlineStore> GetById(int id)
        {
            return _repository.GetOneAsync<OnlineStore>(s => s.Id == id, NavigationProperties);
        }

        public async Task<OnlineStore> CreateAsync(OnlineStore store)
        {
            _repository.Create(store);
            await _repository.SaveAsync();
            return await _repository.GetOneAsync<OnlineStore>(o => o.Id == store.Id, NavigationProperties);
        }

        public async Task<OnlineStore> UpdateAsync(OnlineStore store)
        {
            _repository.Update(store);
            await _repository.SaveAsync();
            return await _repository.GetOneAsync<OnlineStore>(o => o.Id == store.Id, NavigationProperties);
        }

        public Task Delete(OnlineStore store)
        {
            store.IsDeleted = true;
            _repository.Update(store);
            return _repository.SaveAsync();
        }
    }
}
