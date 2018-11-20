using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stellmart.Api.Business.Managers.Interfaces;
using Stellmart.Api.Context.Entities;
using Stellmart.Api.Data.Contract;
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

        public Task<UserSignature> GetUserSignatureAsync(GetSignatureModel data)
        {
            return _repository.GetOneAsync<UserSignature>(
                s => s.Transaction.Phase.ContractId == data.ContractId &&
                s.SignerId == data.UserId &&
                s.Transaction.Phase.SequenceNumber == data.PhaseNumber);
        }
    }
}
