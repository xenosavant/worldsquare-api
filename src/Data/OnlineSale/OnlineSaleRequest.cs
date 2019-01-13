using Stellmart.Api.Data.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Data.OnlineSale
{
    public class OnlineSaleRequest : SignatureRequest
    {
        public int LocationId { get; set; }
        public int PaymentMethodId { get; set; }
    }
}
