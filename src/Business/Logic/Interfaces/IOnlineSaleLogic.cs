using System.Threading.Tasks;
using Stellmart.Api.Data.OnlineSale;
using Stellmart.Api.Data.Enums;
using Stellmart.Api.Context;
using System.Collections.Generic;
using Stellmart.Api.Context.Entities;

namespace Stellmart.Api.Business.Logic.Interfaces
{
    public interface IOnlineSaleLogic
    {
        Task<OnlineSaleData> Checkout(ApplicationUser user, string password);
        Task<PaymentResult> MakePayment(ApplicationUser user, List<OnlineSale> sales, string password, int paymentMethodId);
        Task<PaymentResult> ValidatePayment(ApplicationUser user, List<OnlineSale> sales, string password, int paymentMethodId);
    }
}