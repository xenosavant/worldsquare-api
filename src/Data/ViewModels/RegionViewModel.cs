using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Data.ViewModels
{
    public class RegionViewModel
    {
        [Required]
        public string LocationComponents { get; set; }
    }
}
