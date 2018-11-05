using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stellmart.Api.Business.Managers.Interfaces;
using Stellmart.Api.Context.Entities;
using Stellmart.Api.DataAccess;

namespace Stellmart.Api.Business.Managers
{
    public class SignatureDataManager : ISignatureDataManager
    {
        private readonly IRepository _repository;

        public SignatureDataManager(IRepository repository)
        {
            _repository = repository;
        }

        public Task<Signature> GetAsync(int id)
        {
            return _repository.GetByIdAsync<Signature>(id);
        }
    }
}
