using AutoMapper;
using Stellmart.Api.Business.Managers.Interfaces;
using Stellmart.Api.Context.Entities.ReadOnly;
using Stellmart.Api.Data.Account;
using Stellmart.Api.DataAccess;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Managers
{
    public class SecurityQuestionDataManager : ISecurityQuestionDataManager
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public SecurityQuestionDataManager(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyCollection<SecurityQuestionModel>> GetSecurityQuestionsAsync()
        {
            return _mapper.Map<IReadOnlyCollection<SecurityQuestionModel>>(await _repository.GetAllAsync<SecurityQuestion>());
        }
    }
}
