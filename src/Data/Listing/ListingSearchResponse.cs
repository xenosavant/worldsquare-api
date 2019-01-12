using Stellmart.Api.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Data.Listing
{
    public class ListingSearchResponse
    {
        public List<ListingViewModel> Listings { get; set; }
        public int Count { get; set; } 
    }
}
