﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Data.ViewModels
{
    public class ThreadViewModel
    {
        public int Id { get; set; }
        public ListingDetailViewModel Listing { get; set; }
        public List<MessageViewModel> Messages { get; set; }
    }
}
