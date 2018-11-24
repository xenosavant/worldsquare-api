using Stellmart.Api.Context.Entities.ReadOnly;
using System.Collections.Generic;
using System.Threading.Tasks;
using Stellmart.Api.Data.Account;

namespace Stellmart.Api.Business.Managers.Interfaces
{
    public interface ISecurityQuestionDataManager
    {
        Task<IReadOnlyCollection<SecurityQuestion>> GetSecurityQuestionsAsync();
        Task<IReadOnlyCollection<string>> GetSecurityQuestionsForUserAsync(SecurityQuestionsRequest request);
    }
}
