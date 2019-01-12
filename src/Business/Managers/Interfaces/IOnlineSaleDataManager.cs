using Stellmart.Api.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Managers.Interfaces
{
    public interface IOnlineSaleDataManager
    {
        Task<OnlineSale> CreateAsync(OnlineSale sale);
    }
}
