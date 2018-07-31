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
                Verified = false,
                UserId = 1,
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
            if (viewModel.NativeCurrency != null)
            {
                onlineStore.NativeCurrencyId = viewModel.NativeCurrency.Id;
            }
            _repository.Create(onlineStore);
            await _repository.SaveAsync();
            return await _repository.GetOneAsync<OnlineStore>(o => o.Id == onlineStore.Id, NavigationProperties);
        }

        public async Task<OnlineStore> UpdateAsync(OnlineStore store)
        {
            _repository.Update(store);
            await _repository.SaveAsync();
            return await _repository.GetOneAsync<OnlineStore>(o => o.Id == store.Id, NavigationProperties);
        }

        public async Task DeleteAsync(OnlineStore store)
        {
            store.IsActive = false;
            _repository.Update(store);
            await _repository.SaveAsync();
        }
    }
}
