using AutoMapper;
using Stellmart.Api.Business.Managers.Interfaces;
using Stellmart.Api.Context;
using Stellmart.Api.Context.Entities;
using Stellmart.Api.Data.Enums;
using Stellmart.Api.Data.Kyc;
using Stellmart.Api.DataAccess;
using System;
using System.Threading.Tasks;

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

        public async Task CreateAsync(KycProfileModel model, int createdBy)
        {
            using (var transaction = await _repository.BeginTransactionAsync())
            {
                try
                {
                    _repository.Create(_mapper.Map<KycData>(model), createdBy);

                    var user = await _repository.GetOneAsync<ApplicationUser>(x => x.Id == createdBy);
                    user.VerificationLevelId = (byte)VerificationLevelTypes.LevelOne;
                    _repository.Update(user);

                    await _repository.SaveAsync();
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw new Exception(e.Message);
                }
            }
        }
    }
}
