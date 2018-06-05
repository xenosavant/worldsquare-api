using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities
{
    public class ShippingManifestLineItem
    {
        public int ShippingManifestId { get; set; }

        public int LineItemId { get; set; }

        public virtual ShippingManifest Manifest { get; set; }

        public virtual LineItem LineItem { get; set; }
    }
}
