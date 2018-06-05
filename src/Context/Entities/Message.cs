using Stellmart.Api.Context.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities
{
    public class Message : Entity<int>
    {
        public int ThreadId { get; set; }

        public int PosterId { get; set; }

        public DateTime PostedOn { get; set; }

        public string Body { get; set; }

        public virtual ApplicationUser Poster { get; set; }

        public virtual MessageThread MessageThread { get; set; }
    }
}
