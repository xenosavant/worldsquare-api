using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Stellmart.Api.Context.Entities;
using Stellmart.Api.Data.Kyc;
using Stellmart.Api.DataAccess;

namespace Stellmart.Api.Business.Managers
{
    public class KycDataManager : IKycDataManager
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public KycDataManager(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> CreateAsync(KycProfileModel model, int createdBy)
        {
            _repository.Create(_mapper.Map<KycData>(model), createdBy);
            return await _repository.SaveAsync();
        }
    }
}
