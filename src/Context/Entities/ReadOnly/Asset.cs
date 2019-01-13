using Stellmart.Api.Context.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities.ReadOnly
{
    public class Asset : Entity<int>, IEntity
    {
        public string IssuingAccountId { get; set; }
        public string Code { get; set; }
        public int? CurrencyId { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual ICollection<Contract> Contracts { get; set; }
    }
}
