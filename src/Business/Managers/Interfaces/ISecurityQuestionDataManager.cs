using Stellmart.Api.Data.Account;
using System.Collections.Generic;
using System.Threading.Tasks;
using Stellmart.Api.Context.Entities.ReadOnly;

namespace Stellmart.Api.Business.Managers.Interfaces
{
    public interface ISecurityQuestionDataManager
    {
        Task<IReadOnlyCollection<SecurityQuestion>> GetSecurityQuestionsAsync();
    }
}
