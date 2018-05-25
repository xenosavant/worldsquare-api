using Stellmart.Api.Data.Horizon;

namespace Stellmart.Api.Services
{
    public interface IHorizonService
    {
        HorizonKeyPairModel CreateAccount();
    }
}
