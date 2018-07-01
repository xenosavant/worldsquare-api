using Stellmart.Api.Data.Kyc;
using System.Threading.Tasks;
using Yoti.Auth;

namespace Stellmart.Api.Services
{
    public interface IKycService
    {
        Task<bool> VerifyAsync(KycRequest request);
    }
}
