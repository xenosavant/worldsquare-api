using Stellmart.Api.Data.Account;
using System.Threading.Tasks;

namespace Stellmart.Api.Services
{
    public interface IAccountService
    {
        Task<SignupResponse> GetSignupResponseAsync();
    }
}
