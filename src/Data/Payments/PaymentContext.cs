using Stellmart.Api.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stellmart.Api.Context;
using Stellmart.Api.Context.Entities;
using Stellmart.Api.Context.Entities.ReadOnly;

namespace Stellmart.Api.Data.Payments
{
    public class PaymentContext
    {
        public ApplicationUser User { get; set; }
        public List<Contract> Contracts { get; set; }
        public IHorizonService HorizonService { get; set; }
        public Asset Asset { get; set; }
        public string SecretKey { get; set; }
    }
}
