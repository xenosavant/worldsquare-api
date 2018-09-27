using Stellmart.Api.Context.Entities.ReadOnly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Data.ViewModels
{
    public class ItemMetaDataViewModel
    {
        public int ItemConditionId { get; set; }

        public string [] KeyWords { get; set; }

        public int [] CategoryIds { get; set; }
    }
}
