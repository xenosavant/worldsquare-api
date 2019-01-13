using Stellmart.Api.Data.Enums;
using Stellmart.Api.Data.Payments;
using Stellmart.Api.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities.Payment
{
    public interface IPaymentStrategy
    {
        Task<PaymentResult> ValidatePayment(PaymentContext context);
        Task<PaymentResult> InitiatePayment(PaymentContext context);
    }
}
