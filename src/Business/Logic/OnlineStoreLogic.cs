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

namespace Stellmart.Api.Business.Logic
{
    public class OnlineStoreLogic : IOnlineStoreLogic
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;
        public static string NavigationProperties => "User,NativeCurrency,ServiceRegion";

        public OnlineStoreLogic(IRepository repository, IMapper mapper) 
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OnlineStore>> GetAllAsync()
        {
            return await _repository.GetAllAsync<OnlineStore>();
        }

        public async Task<OnlineStore> GetByIdAsync(int id)
        {
            return await _repository.GetOneAsync<OnlineStore>(s => s.Id == id, NavigationProperties);
        }

        public async Task<OnlineStore> CreateAsync(int userId, OnlineStoreViewModel viewModel)
        {
            var onlineStore = new OnlineStore()
            {
                Name = viewModel.Name,
                Description = viewModel.Description,
                TagLine = viewModel.TagLine,
                UserId = userId,
                NativeCurrencyId = viewModel.NativeCurrency.Id
            };
            if (viewModel.ServiceRegion != null)
            {
                onlineStore.ServiceRegion =
                    new Region()
                    {
                        LocationComponents = viewModel.ServiceRegion.LocationComponents
                    };
            }
            _repository.Create(onlineStore);
            await _repository.SaveAsync();
            return await _repository.GetOneAsync<OnlineStore>(o => o.Id == onlineStore.Id, NavigationProperties);
        }

        public async Task<OnlineStore> UpdateAsync(OnlineStoreViewModel vm, OnlineStore store)
        {
            store.Name = vm.Name;
            store.Description = vm.Description;
            store.TagLine = vm.TagLine;
            var serviceRegionViewModel = _mapper.Map<RegionViewModel>(store.ServiceRegion);
            var regionDiff = Validation.GetPropertyDiff(vm.ServiceRegion, serviceRegionViewModel);
            if (regionDiff.Count() > 0)
            {
                store.ServiceRegion.LocationComponents = vm.ServiceRegion.LocationComponents;
                _repository.Update(store.ServiceRegion);
            }

            var currencyViewModel = _mapper.Map<CurrencyViewModel>(store.NativeCurrency);
            var currencyDiff = Validation.GetPropertyDiff(vm.NativeCurrency, currencyViewModel);
            if (currencyDiff.Count() > 0)
            {
                store.NativeCurrencyId = vm.NativeCurrency.Id;
                store.NativeCurrency = null;
            }

            _repository.Update(store);
            await _repository.SaveAsync();
            if (store.NativeCurrency == null)
            {
                store.NativeCurrency = await _repository.GetByIdAsync<Currency>(store.NativeCurrencyId);
            }
            return store;
        }

        public async Task DeleteAsync(OnlineStore store)
        {
            store.IsActive = false;
            _repository.Update(store);
            await _repository.SaveAsync();
        }
    }
}
