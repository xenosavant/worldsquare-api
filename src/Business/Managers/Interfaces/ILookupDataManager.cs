using Stellmart.Api.Context.Entities.ReadOnly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Managers.Interfaces
{
    public interface ILookupDataManager
    {
        Task<IEnumerable<T>> GetAsync<T>(List<int> ids = null)
            where T : LookupData;
    }
}
