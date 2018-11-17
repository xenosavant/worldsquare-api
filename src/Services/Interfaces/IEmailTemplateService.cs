using Stellmart.Api.Data.Account;
using System.Threading.Tasks;
using Stellmart.Api.Context;

namespace Stellmart.Api.Services.Interfaces
{
    public interface IEmailTemplateService
    {
        Task SendChangePasswordAsync(ChangePasswordRequest model);
        Task SendGettingStartedAsync(ApplicationUser user);
        Task SendResetPasswordAsync(string email, string newPassword);
        Task SendWelcomeMailAsync(ApplicationUser user, string code);
        Task SendConfirmEmailAsync(string email, string subject, string body);
        Task SendForgotPasswordEmailAsync(string email, string subject, string body);
    }
}
