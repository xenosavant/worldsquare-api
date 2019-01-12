using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities = Stellmart.Api.Context.Entities;

namespace Stellmart.Api.Data.Listing
{
    public class ListingSearchDto
    {
        public List<Entities.Listing> Listings { get; set; }
        public int Count { get; set; }
    }
}
