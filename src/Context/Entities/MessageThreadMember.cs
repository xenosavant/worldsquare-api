using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities
{
    public class MessageThreadMember
    {
        public int MessageThreadId { get; set; }
        public int UserId { get; set; }
        public virtual MessageThread Thread { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
