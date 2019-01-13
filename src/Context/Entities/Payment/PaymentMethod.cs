using Stellmart.Api.Context.Entities.Entity;
using Stellmart.Api.Context.Entities.ReadOnly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities
{
    public abstract class PaymentMethod : Entity<int>, IEntity
    {
        public string DisplayText { get; set; }

        public virtual ICollection<UserPaymentMethod> UserPaymentMethods { get; set; }
    }
}
