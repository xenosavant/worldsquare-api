using Stellmart.Api.Data.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Data.Checkout
{
    public class CheckoutRequest : SignatureRequest
    {
        public int LocationId { get; set; }
        public int PaymentTypeId { get; set; }
    }
}
