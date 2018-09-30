using AutoMapper;
using Stellmart.Api.Context.Entities;
using Stellmart.Api.Data;
using Stellmart.Api.Data.ViewModels;
using Stellmart.Api.DataAccess;
using System.Collections.Generic;
using System.Threading.Tasks;
using Stellmart.Api.Business.Managers.Interfaces;

namespace Stellmart.Api.Business.Logic
{
    public class OnlineStoreLogic : IOnlineStoreLogic
    {
        private readonly IOnlineStoreDataManager _manager;
        private readonly IMapper _mapper;

        public OnlineStoreLogic(IOnlineStoreDataManager manager, IMapper mapper) 
        {
            _manager = manager;
            _mapper = mapper;
        }

        public Task<IEnumerable<OnlineStore>> GetAll()
        {
            return _manager.GetAll();
        }

        public Task<OnlineStore> GetById(int id)
        {
            return _manager.GetById(id);
        }

        public Task<OnlineStore> Create(int userId, OnlineStoreViewModel viewModel)
        {
            var onlineStore = _mapper.Map<OnlineStore>(viewModel);
            onlineStore.UserId = userId;
            if (viewModel.ServiceRegion != null)
            {
                onlineStore.ServiceRegion =
                    new Region()
                    {
                        LocationComponents = viewModel.ServiceRegion.LocationComponents
                    };
            }
            if (viewModel.NativeCurrency != null)
            {
                onlineStore.NativeCurrencyId = viewModel.NativeCurrency.Id;
            }
            return _manager.CreateAndSaveAsync(onlineStore, userId);
        }

        public Task<OnlineStore> Update(int userId, OnlineStore store, Delta<OnlineStore> delta)
        {
            delta.Patch(store);
            return _manager.UpdateAndSaveAsync(store, userId);
        }

        public Task Delete(OnlineStore store)
        {
            return _manager.Delete(store);
        }
    }
}
