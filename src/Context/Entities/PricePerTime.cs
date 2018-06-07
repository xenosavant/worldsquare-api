using Stellmart.Api.Context.Entities.Entity;
using Stellmart.Api.Context.Entities.ReadOnly;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities
{
    public class PricePerTime : Entity<int>
    {
        [Required]
        public int CurrencyAmountId { get; set; }

        [Required]
        public int TimeUnitId { get; set; }

        public virtual CurrencyAmount Amount { get; set; }

        public virtual TimeUnit TimeUnit { get; set; }

    }
}
