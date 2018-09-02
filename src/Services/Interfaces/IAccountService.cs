using Stellmart.Api.Data.Account;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stellmart.Api.Services.Interfaces
{
    public interface IAccountService
    {
        Task<bool> SignupAsync(ApplicationUserModel model);
        Task<IReadOnlyCollection<SecurityQuestionModel>> GetSecurityQuestionsAsync();
    }
}
