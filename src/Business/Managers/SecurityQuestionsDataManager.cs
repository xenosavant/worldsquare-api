using AutoMapper;
using Stellmart.Api.Business.Managers.Interfaces;
using Stellmart.Api.Context.Entities.ReadOnly;
using Stellmart.Api.Data.Account;
using Stellmart.Api.DataAccess;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Managers
{
    public class SecurityQuestionsDataManager : ISecurityQuestionDataManager
    {
        private readonly IReadOnlyRepository _repository;
        private readonly IMapper _mapper;

        public SecurityQuestionsDataManager(IReadOnlyRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyCollection<SecurityQuestionModel>> GetSecurityQuestions()
        {
            return _mapper.Map<IReadOnlyCollection<SecurityQuestionModel>>(await _repository.GetAllAsync<SecurityQuestion>());
        }
    }
}
