using Stellmart.Api.Data.Account;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Managers.Interfaces
{
    public interface ISecurityQuestionDataManager
    {
        Task<IReadOnlyCollection<SecurityQuestionModel>> GetSecurityQuestions();
    }
}
