using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities.ReadOnly
{
    public class ContractType : LookupData
    {
        public virtual ICollection<Contract> Contracts { get; set; }
    }
}
