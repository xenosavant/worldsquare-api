using Stellmart.Api.Business.Managers.Interfaces;
using Stellmart.Api.Context.Entities.ReadOnly;
using Stellmart.Api.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Managers
{
    public class LookupDataManager : ILookupDataManager
    {
        private readonly IRepository _repository;

        public LookupDataManager(IRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<T>> GetAsync<T>(List<int> ids = null)
            where T : LookupData
        {
            if (ids != null)
            {
                return _repository.GetAsync<T>(l => ids.Contains(l.Id));
            }
            else
            {
                return _repository.GetAsync<T>();
            }
        }
    }
}
