using Stellmart.Api.Context.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities
{
    public class OrderItem : Entity<int>
    {
        public int LineItemId { get; set; }
        public int OrderId { get; set; }
        public bool Fulfilled { get; set; }
        public virtual LineItem Item { get; set; }
        public virtual Order Order { get; set; }
    }
}
