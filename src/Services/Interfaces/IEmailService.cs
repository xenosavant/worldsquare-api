using System.Threading.Tasks;
using Stellmart.Api.Data.Email;

namespace Stellmart.Api.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(LogEmailModel model);
        Task<string> ReadEmailTemplateFromHttpAsync(string url);
    }
}
