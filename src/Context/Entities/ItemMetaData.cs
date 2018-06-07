using Stellmart.Api.Context.Entities.Entity;
using Stellmart.Api.Context.Entities.ReadOnly;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context
{
    public class ItemMetaData : Entity<int>
    {
        public int SuperCategoryId { get; set; }

        public int SubCategoryId { get; set; }

        public int ListingCategoryId { get; set; }

        public string KeyWords { get; set; }

        public int itemConditionId { get; set; }

        public ItemCondition ItemCondition { get; set; }

        public virtual SuperCategory SuperCategory { get; set; }

        public virtual SubCategory SubCategory { get; set; }

        public virtual ListingCategory ListingCategory { get; set; }
    }
}
