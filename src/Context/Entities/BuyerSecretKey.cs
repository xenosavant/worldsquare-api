using Stellmart.Api.Context.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities
{
    public class BuyerSecretKey : SecretKey
    {
        public int UserId { get; set; }

        public int OrderId { get; set; }

        public virtual ApplicationUser Buyer { get; set; }
    }
}
