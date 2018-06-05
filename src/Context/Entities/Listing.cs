using Stellmart.Api.Context.Entities.Entity;
using Stellmart.Api.Context.Entities.ReadOnly;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities
{
    public class Listing : EntityMaximum
    {
        [Required]
        public int StoreId { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public bool Flagged { get; set; }

        public int? ThreadId { get; set; }

        [Required]
        public int SuperCategoryId { get; set; }

        [Required]
        public int SubCategoryId { get; set; }

        [Required]
        public int ListingCategoryId { get; set; }

        public string KeyWords { get; set; }

        [Required]
        public bool Internal { get; set; }

        [ForeignKey("StoreId")]
        public virtual OnlineStore Store { get; set; }

        [Required]
        public virtual SuperCategory SuperCategory{ get; set; }

        [Required]
        public virtual SubCategory SubCategory { get; set; }

        [Required]
        public virtual ListingCategory ListingCategory { get; set; }

        public virtual ICollection<MessageThread> Threads { get; set; }
    }
}
