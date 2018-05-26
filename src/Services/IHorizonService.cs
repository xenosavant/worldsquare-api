using Stellmart.Api.Data.Horizon;

namespace Stellmart.Api.Services
{
    public interface IHorizonService
    {
        HorizonKeyPairModel CreateAccount();
	 void Fund_Test_Account(string Public_Key);
    }
}
