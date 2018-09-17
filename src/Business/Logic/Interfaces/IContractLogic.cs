using Stellmart.Api.Context.Entities;
using Stellmart.Api.Data.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Logic.Interfaces
{
    interface IContractLogic
    {
        Task<Contract> GetByIdAsync(int id);
        Task<Contract> CreateAsync(Contract contract);
        Task<Contract> UpdateAsync(Contract contract);
    }
}
