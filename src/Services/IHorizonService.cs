using Stellmart.Api.Data.Horizon;

namespace Stellmart.Api.Services
{
    public interface IHorizonService
    {
        void CreateAccount(HorizonKeyPairModel data);
    }
}
