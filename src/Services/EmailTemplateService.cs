using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Stellmart.Api.Data.Account;
using Stellmart.Api.Data.Email;
using Stellmart.Api.Data.Enums;
using Stellmart.Api.Data.Settings;
using Stellmart.Api.Services.Interfaces;

namespace Stellmart.Api.Services
{
    public class EmailTemplateService : IEmailTemplateService
    {
        private readonly IEmailService _emailService;
        private readonly IOptions<HostSettings> _hostSettings;

        public EmailTemplateService
        (
            IEmailService emailService,
            IOptions<HostSettings> hostSettings
        )
        {
            _emailService = emailService;
            _hostSettings = hostSettings;
        }

        public async Task SendChangePasswordAsync(ChangePasswordRequest model)
        {
            var body = new StringBuilder();
            body.Append("You recently requested your password change.<br />");
            body.Append("Your password for account " + model.Email + " has been updated.");

            var subject = "Password change for your account";

            var emailModel = new LogEmailModel
            {
                To = model.Email,
                Subject = subject,
                Content = body.ToString(),
                Type = EmailMessageTypes.ChangePasswordEmail,
                From = " _defaultConfig.OutgoingEmailAddress"
            };

            await _emailService.SendEmailAsync(emailModel);
        }

        public async Task SendConfirmEmailAsync(string email, string subject, string body)
        {
            var emailModel = new LogEmailModel
            {
                To = email,
                Subject = subject,
                Content = body,
                Type = EmailMessageTypes.ConfirmAccountEmail,
                From = " _defaultConfig.OutgoingEmailAddress"
            };

            await _emailService.SendEmailAsync(emailModel);
        }

        public async Task SendForgotPasswordEmailAsync(string email, string subject, string body)
        {
            var emailModel = new LogEmailModel
            {
                To = email,
                Subject = subject,
                Content = body,
                Type = EmailMessageTypes.ForgotPasswordEmail,
                From = " _defaultConfig.OutgoingEmailAddress"
            };

            await _emailService.SendEmailAsync(emailModel);
        }

        public Task SendGettingStartedAsync(string userId)
        {
            throw new System.NotImplementedException();
        }

        public Task SendResetPasswordAsync(string email, string newPassword)
        {
            throw new System.NotImplementedException();
        }

        public Task SendWelcomeMailAsync(string userId, string code)
        {
            throw new System.NotImplementedException();
        }
    }
}
