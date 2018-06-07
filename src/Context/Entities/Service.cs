using Stellmart.Api.Context.Entities.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities
{
    public abstract class Service : EntityMaximum
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string TagLine { get; set; }

        public int? UserId { get; set; }

        [Required]
        public int NativeCurrencyId { get; set; }

        [Required]
        bool Verified { get; set; }

    }
}
