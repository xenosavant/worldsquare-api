using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities.ReadOnly
{
    public class Category : LookupData
    {
        public int? ParentCategoryId { get; set; }

        public Category ParentCategory { get; set; }

        public Category ChildCategory { get; set; }

        public ICollection<ItemMetaDataCategory> ItemMetaDataCategories { get; set; }
    }
}
