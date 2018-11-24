using Microsoft.AspNetCore.Http;
using Stellmart.Api.Data.Account;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stellmart.Api.Services.Interfaces
{
    public interface IAccountService
    {
        Task<bool> SignupAsync(ApplicationUserModel model, HttpContext httpContext);
        Task<IReadOnlyCollection<SecurityQuestionsResponse>> GetSecurityQuestionsAsync();
        Task<IReadOnlyCollection<SecurityQuestionsResponse>> GetSecurityQuestionsForUserAsync(SecurityQuestionsRequest request);
        Task<bool> ForgotPassword(ForgotPasswordRequest model);
        Task<bool> ResetPassword(ResetPasswordRequest model);
        Task<bool> ConfirmEmail(ConfirmEmailRequest model);
    }
}
