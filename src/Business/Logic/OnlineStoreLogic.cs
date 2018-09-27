using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Stellmart.Api.Context.Entities;
using Stellmart.Api.Context.Entities.ReadOnly;
using Stellmart.Api.Controllers.Helpers;
using Stellmart.Api.Data.ViewModels;
using Stellmart.Api.DataAccess;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stellmart.Api.Data;
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
            return _manager.CreateAsync(onlineStore);
        }

        public Task<OnlineStore> Update(OnlineStore store, Delta<OnlineStore> delta)
        {
            delta.Patch(store);
            return _manager.UpdateAsync(store);
        }

        public Task Delete(OnlineStore store)
        {
            return _manager.Delete(store);
        }
    }
}
