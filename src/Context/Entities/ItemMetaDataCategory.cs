using Stellmart.Api.Context.Entities.ReadOnly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities
{
    public class ItemMetaDataCategory
    {
        public int ItemMetaDataId { get; set; }
        public int CategoryId { get; set; }
        public ItemMetaData ItemMetaData { get; set; }
        public Category Category { get; set; }
    }
}
