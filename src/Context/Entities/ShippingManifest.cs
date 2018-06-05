using Stellmart.Api.Context.Entities.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities
{
    public class ShippingManifest : Entity<int>
    {

        public IEnumerable<LineItem> LineItems
        {
            get
            {
                if (ShippingManifestLineItems != null)
                {
                    return ShippingManifestLineItems.Select(smli => smli.LineItem);
                }
                else return null;
            }
        }

        public virtual ICollection<ShippingManifestLineItem> ShippingManifestLineItems { get; set; }
    }
}
