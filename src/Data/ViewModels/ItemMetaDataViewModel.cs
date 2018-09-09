using Stellmart.Api.Context.Entities.ReadOnly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Data.ViewModels
{
    public class ItemMetaDataViewModel
    {
        public int SuperCategoryId { get; set; }

        public int SubCategoryId { get; set; }

        public int ListingCategoryId { get; set; }

        public string KeyWords { get; set; }

        public int ItemConditionId { get; set; }

        public ReadonlyViewModel ItemCondition { get; set; }

        public virtual ReadonlyViewModel SuperCategory { get; set; }

        public virtual ReadonlyViewModel SubCategory { get; set; }

        public virtual ReadonlyViewModel ListingCategory { get; set; }
    }
}
