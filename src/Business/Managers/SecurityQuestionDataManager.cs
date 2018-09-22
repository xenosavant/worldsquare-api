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

        public SecurityQuestionDataManager(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyCollection<SecurityQuestion>> GetSecurityQuestionsAsync()
        {
            return await _repository.GetAllAsync<SecurityQuestion>() as IReadOnlyCollection<SecurityQuestion>;
        }
    }
}
