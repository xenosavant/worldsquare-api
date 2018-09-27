using Stellmart.Api.Context.Entities;
using Stellmart.Api.Context.Entities.Entity;
using Stellmart.Api.Context.Entities.ReadOnly;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Stellmart.Api.Business.Extensions;

namespace Stellmart.Api.Context.Entities
{
    public class ItemMetaData : Entity<int>
    {
        public string KeyWords { get; set; }

        public int ItemConditionId { get; set; }

        public virtual ItemCondition ItemCondition { get; set; }

        public virtual TradeItem TradeItem { get; set; }

        public virtual Listing Listing { get; set; }

        public ICollection<ItemMetaDataCategory> ItemMetaDataCategories { get; set; } = new List<ItemMetaDataCategory>();

        public ICollection<Category> Categories { get { return ItemMetaDataCategories?.Select(i => i.Category).ToList().SortCategories(); } }
    }
}
