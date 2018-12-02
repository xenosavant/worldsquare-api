using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities
{
    public class ListingMessageThread
    {
        public int ListingId { get; set; }
        public int MessageThreadId { get; set; }
        public virtual Listing Listing { get; set; }
        public virtual MessageThread MessageThread { get; set; }
    }
}
