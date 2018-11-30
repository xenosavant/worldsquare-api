using Stellmart.Api.Data.Checkout;
using Stellmart.Api.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Logic.Interfaces
{
    interface ICheckoutLogic
    {
        Task<CheckoutData> ManagedCheckout(int userId, string password,
            int nativeCurrencyTypeId = (int)NativeCurrencyTypes.Default);
    }
}
