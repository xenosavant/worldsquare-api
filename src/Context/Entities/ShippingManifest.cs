using Stellmart.Api.Context.Entities.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities
{
    public class ShippingManifest : Entity<int>
    {
        [NotMapped]
        public IEnumerable<LineItem> LineItems => ShippingManifestLineItems?.Select(s => s.LineItem);

        public virtual ICollection<ShippingManifestLineItem> ShippingManifestLineItems { get; set; }
    }
}
