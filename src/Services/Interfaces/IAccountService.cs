using Stellmart.Api.Data.Account;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Stellmart.Api.Data;

namespace Stellmart.Api.Services.Interfaces
{
    public interface IAccountService
    {
        Task<bool> SignupAsync(ApplicationUserModel model, HttpContext httpContext);
        Task<IReadOnlyCollection<SecurityQuestionModel>> GetSecurityQuestionsAsync();
    }
}
