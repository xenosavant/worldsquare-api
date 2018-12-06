using Stellmart.Api.Context.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities
{
    public class MessageThread : EntityMaximum
    {
        public int ListingId { get; set; }

        public virtual ApplicationUser Initiator { get; set; }

        public virtual ICollection<Message> Messages { get; set; }

        public virtual Listing Listing { get; set; }

        public virtual ICollection<MessageThreadMember> MessageThreadMembers { get; set; }
    }
}
