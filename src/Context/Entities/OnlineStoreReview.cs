using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities
{
    public class OnlineStoreReview
    {
        public int OnlineStoreId { get; set; }
        public int ReviewId { get; set; }
        public virtual OnlineStore OnlineStore { get; set; }
        public virtual Review Review { get; set; }
    }
}
