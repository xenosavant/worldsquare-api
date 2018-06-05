using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stellmart.Api.Context.Entities.ReadOnly;
using System.ComponentModel.DataAnnotations.Schema;
using Stellmart.Api.Context.Entities.Entity;

namespace Stellmart.Api.Context.Entities
{
    public class InventoryItem : AuditableEntity<int>
    {
        public int ConditionId { get; set; }

        public int UnitPriceId { get; set; }

        public int UnitTypeId { get; set; }

        public string Descriptors { get; set; }

        public string UPC { get; set; }

        public string SKU { get; set; }

        public int TradeInValueId { get; set; }

        public int TradeInStateId { get; set; }

        public int UnitsAvailable { get; set; }

        public int UnitsSold { get; set; }

        public int UnitsReturned { get; set; }

        public int ListingId { get; set; }

        public Condition Condition { get; set; }

        [ForeignKey("UnitPriceId")]
        public virtual CurrencyAmount Price { get; set; }

        [ForeignKey("TradeInValueId")]
        public virtual CurrencyAmount TradeInValue { get; set; }

        public virtual QuantityUnit UnitType { get; set; }

        public virtual Listing Listing { get; set; }

        public virtual DistributionCenter DistributionCenter { get; set; }

        public virtual TradeInState TradeInState { get; set; }
    }
}
