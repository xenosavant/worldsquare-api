using System.Threading.Tasks;
using Stellmart.Api.Data.Checkout;
using Stellmart.Api.Data.Enums;

namespace Stellmart.Api.Business.Logic.Interfaces
{
    public interface ICheckoutLogic
    {
        Task<CheckoutData> ManagedCheckout(int userId, string password,
            int nativeCurrencyTypeId = (int) NativeCurrencyTypes.Default);
    }
}