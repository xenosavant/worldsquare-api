using Stellmart.Api.Business.Managers.Interfaces;
using Stellmart.Api.Context.Entities;
using Stellmart.Api.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Managers
{
    public class OnlineSaleDataManager : IOnlineSaleDataManager
    {
        private readonly IRepository _repository;

        public OnlineSaleDataManager(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<OnlineSale> CreateAsync(OnlineSale sale)
        {
            _repository.Create(sale);
            await _repository.SaveAsync();
            return sale;
        }
    }
}
