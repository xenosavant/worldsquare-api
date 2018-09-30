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

        public OnlineStore Create(OnlineStore store, int id)
        {
            _repository.Create(store, id);
            return store;
        }

        public async Task<OnlineStore> CreateAndSaveAsync(OnlineStore store, int id)
        {
            _repository.Create(store, id);
            await _repository.SaveAsync();
            return store;
        }

        public OnlineStore Update(OnlineStore store, int id)
        {
            _repository.Update(store, id);
            return store;
        }

        public async Task<OnlineStore> UpdateAndSaveAsync(OnlineStore store, int id)
        {
            _repository.Update(store);
            await _repository.SaveAsync();
            return store;
        }

        public Task Delete(OnlineStore store)
        {
            store.IsDeleted = true;
            _repository.Update(store);
            return _repository.SaveAsync();
        }
    }
}
