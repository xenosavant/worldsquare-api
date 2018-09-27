using Stellmart.Api.Context.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities
{
    public class ContractSecretKey : AuditableEntity<int>
    {
        public string SecretKey { get; set; }

        public int UserId { get; set; }

        public int ContractId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual Contract Contract { get; set; }
    }
}
