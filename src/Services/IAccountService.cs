using Stellmart.Api.Data.Account;
using Stellmart.Data.Account;
using System.Threading.Tasks;

namespace Stellmart.Api.Services
{
    public interface IAccountService
    {
        Task SignupAsync(SignupRequest request);
        Task<SignupResponse> GetSignupResponseAsync();
    }
}
