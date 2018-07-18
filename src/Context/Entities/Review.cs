﻿using Stellmart.Api.Context.Entities.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities
{
    public class Review : Entity<int>
    {
        public int ReviewerId { get; set; }

        public int Stars { get; set; }

        public string Body { get; set; }

        public virtual ApplicationUser Reviewer { get; set; }

        public virtual ICollection<OnlineStoreReview> OnlineStoreReviews { get; set; }
    }
}