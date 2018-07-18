﻿using Stellmart.Api.Context.Entities.Entity;
using Stellmart.Api.Context.Entities.ReadOnly;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities
{
    public class CurrencyAmount : AuditableEntity<int>
    {
        [Required]
        public int CurrencyTypeId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public virtual Currency CurrencyType { get; set; }

        public virtual TradeItem TradeItem { get; set; }

        public virtual PricePerDistance PricePerDistance { get; set; }

        public virtual PricePerTime PricePerTime { get; set; }
    }
}