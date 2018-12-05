using Stellmart.Api.Business.Managers.Interfaces;
using Stellmart.Api.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Managers
{
    public class ThreadDataManager : IThreadDataManager
    {
        private readonly IRepository _repository;

        public ThreadDataManager(IRepository repository)
        {
            _repository = repository;
        }


    }
}
