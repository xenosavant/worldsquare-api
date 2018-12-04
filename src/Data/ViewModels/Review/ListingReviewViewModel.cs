using Stellmart.Api.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Data.ViewModels
{
    public class ListingReviewViewModel : ReviewViewModel
    {
        public string OnlineStoreName { get; set; }
        public ListingDetailViewModel Listing { get; set; }
    }
}
