using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using Stellmart.Api.Data.Email;
using Stellmart.Api.Data.Settings;
using Stellmart.Api.Services.Interfaces;
using System.Threading.Tasks;

namespace Stellmart.Api.Services
{
    public class EmailService : IEmailService
    {
        private readonly IOptions<EmailSettings> _emailSettings;
        private readonly IAzureStorageService _azureStorageService;

        public EmailService(IOptions<EmailSettings> emailSettings, IAzureStorageService azureStorageService)
        {
            _emailSettings = emailSettings;
            _azureStorageService = azureStorageService;
        }

        public async Task SendEmailAsync(LogEmailModel model)
        {
            var client = new SendGridClient(_emailSettings.Value.ApiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(_emailSettings.Value.FromEmail, _emailSettings.Value.FromName),
                Subject = model.Subject,
                PlainTextContent = model.Content,
                HtmlContent = model.Content
            };
            msg.AddTo(new EmailAddress(model.To));
            var response = await client.SendEmailAsync(msg);
        }

        public async Task<string> ReadEmailTemplateFromHttpAsync(string fileName)
        {
            return await _azureStorageService.DownloadTextAsync("tpl/" + fileName, "cdn");
        }
    }
}
