using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities
{
    public class UserPaymentMethod
    {
        public int UserId { get; set; }
        public int PaymentMethodId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
    }
}
