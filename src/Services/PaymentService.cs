using Stellmart.Api.Context;
using Stellmart.Api.Context.Entities;
using Stellmart.Api.Context.Entities.Payment;
using Stellmart.Api.Data.Enums;
using Stellmart.Api.Data.Payments;
using Stellmart.Api.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Services
{
    public class PaymentService : IPaymentService
    {
        private IPaymentStrategy _paymentStrategy;
        private PaymentContext _context;

        public PaymentService(IHorizonService horizonService)
        {
            _context.HorizonService = horizonService;
        }

        public void SetStrategy(IPaymentStrategy strategy)
        {
            _paymentStrategy = strategy;
        }

        public void SetContracts(List<Contract> contracts)
        {
            _context.Contracts = contracts;
        }

        public void SetUser(ApplicationUser user)
        {
            _context.User = user;
        }

        public void SetSecret(string secret)
        {
            _context.SecretKey = secret;
        }

        public async Task<PaymentResult> MakePayment()
        {
            return await _paymentStrategy.InitiatePayment(_context);
        }

        public async Task<PaymentResult> ValidatePayment()
        {

            return await _paymentStrategy.ValidatePayment(_context);
        }
    }
}
