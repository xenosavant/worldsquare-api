using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities.ReadOnly
{
    public class Currency : LookupData
    {
        public int Precision { get; set; }

        public virtual ICollection<Service> Services { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }

        public virtual ICollection<CurrencyAmount> CurrencyAmounts { get; set; }

        public virtual ICollection<Listing> Listings { get; set; }

        public virtual ICollection<Asset> Assets { get; set; }

    }
}
