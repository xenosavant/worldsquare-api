using Stellmart.Api.Data.Account;
using System.Threading.Tasks;

namespace Stellmart.Api.Services.Interfaces
{
    public interface IEmailTemplateService
    {
        Task SendChangePasswordAsync(ChangePasswordRequest model);
        Task SendGettingStartedAsync(string userId);
        Task SendResetPasswordAsync(string email, string newPassword);
        Task SendWelcomeMailAsync(string userId, string code);
        Task SendConfirmEmailAsync(string email, string subject, string body);
        Task SendForgotPasswordEmailAsync(string email, string subject, string body);
    }
}
