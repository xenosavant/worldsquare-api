using Stellmart.Api.Context.Entities;
using Stellmart.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Data.ViewModels
{
    public class OnlineStoreViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string TagLine { get; set; }

        public bool Verified { get; set;  }

        public ApplicationUserViewModel User { get; set; }

        public RegionViewModel ServiceRegion { get; set; }

        public CurrencyViewModel NativeCurrency { get; set; }
    }
}
