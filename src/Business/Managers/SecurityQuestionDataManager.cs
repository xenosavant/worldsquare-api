using Stellmart.Api.Business.Managers.Interfaces;
using Stellmart.Api.Context;
using Stellmart.Api.Context.Entities.ReadOnly;
using Stellmart.Api.Data.Account;
using Stellmart.Api.DataAccess;
using System.Collections.Generic;
using System.Threading.Tasks;
using ServiceStack.Text;

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

        public async Task<IReadOnlyCollection<string>> GetSecurityQuestionsForUserAsync(SecurityQuestionsRequest request)
        {
            var user = await _repository.GetOneAsync<ApplicationUser>(x => x.Id == request.UserId);
            var listQuestions = JsonSerializer.DeserializeFromString<IReadOnlyCollection<string>>(user.SecurityQuestions);

            return listQuestions;
        }
    }
}
