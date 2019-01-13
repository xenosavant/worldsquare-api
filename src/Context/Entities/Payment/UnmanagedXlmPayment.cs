using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stellmart.Api.Data.Enums;
using Stellmart.Api.Data.Payments;

namespace Stellmart.Api.Context.Entities.Payment
{
    public class UnmanagedXlmPayment : PaymentMethod, IPaymentStrategy
    {
        public Task<PaymentResult> InitiatePayment(PaymentContext context)
        {
            throw new NotImplementedException();
        }

        public Task<PaymentResult> ValidatePayment(PaymentContext context)
        {
            throw new NotImplementedException();
        }
    }
}
