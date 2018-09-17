using Stellmart.Api.Business.Logic.Interfaces;
using Stellmart.Api.Context.Entities;
using Stellmart.Api.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Logic
{
    public class ContractLogic : IContractLogic
    {
        private readonly IRepository _repository;

        public ContractLogic(IRepository repository)
        {
            _repository = repository;
        }


        public async Task<Contract> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync<Contract>(id);
        }

        public async Task<Contract> CreateAsync(Contract contract)
        {
            _repository.Create(contract);
            await _repository.SaveAsync();
            return contract;
        }

        public async Task<Contract> UpdateAsync(Contract contract)
        {
            _repository.Update(contract);
            await _repository.SaveAsync();
            return contract;
        }
    }
}
