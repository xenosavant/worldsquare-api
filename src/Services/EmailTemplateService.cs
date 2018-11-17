using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Stellmart.Api.Context;
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

        public Task SendGettingStartedAsync(ApplicationUser user)
        {
            throw new System.NotImplementedException();
        }

        public Task SendResetPasswordAsync(string email, string newPassword)
        {
            throw new System.NotImplementedException();
        }

        public async Task SendWelcomeMailAsync(ApplicationUser user, string code)
        {
            if (user != null)
            {
                // var body = await _emailService.ReadEmailTemplateFromHttpAsync("Welcome.html");
                // body = body.Replace("[customer_name]", user.Email);
                // body = body.Replace("[name_app]", "Worldsquare");

                var callbackUrl = $"{_hostSettings.Value.AppUrl}confirm-email/{user.Id}/{WebUtility.UrlEncode(code)}";

                // body = body.Replace("[confirm_url]", callbackUrl);

                var emailModel = new LogEmailModel
                {
                    To = user.Email,
                    Subject = "Welcome to Worldsquare!",
                    Content = $"Please confirm your email by clicking here: <a href='{callbackUrl}'>link</a>",
                    Type = EmailMessageTypes.NewAccountWelcomeEmail
                };

                await _emailService.SendEmailAsync(emailModel);
            }
        }
    }
}
