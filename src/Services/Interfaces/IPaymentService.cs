using Stellmart.Api.Context;
using Stellmart.Api.Context.Entities;
using Stellmart.Api.Context.Entities.Payment;
using Stellmart.Api.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Services.Interfaces
{
    public interface IPaymentService
    {
        void SetStrategy(IPaymentStrategy strategy);
        void SetContracts(List<Contract> contracts);
        void SetUser(ApplicationUser user);
        void SetSecret(string secret);
        Task<PaymentResult> MakePayment();
        Task<PaymentResult> ValidatePayment();
    }
}
