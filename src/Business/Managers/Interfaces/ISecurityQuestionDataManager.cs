using Stellmart.Api.Context.Entities.ReadOnly;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Managers.Interfaces
{
    public interface ISecurityQuestionDataManager
    {
        Task<IReadOnlyCollection<SecurityQuestion>> GetSecurityQuestionsAsync();
    }
}
