using Stellmart.Api.Context.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities
{
    public class Message : AuditableEntity<int>
    {
        public int MessageThreadId { get; set; }

        public string Body { get; set; }

        public virtual ApplicationUser Poster { get; set; }

        public virtual MessageThread MessageThread { get; set; }
    }
}
