using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Data.ViewModels
{
    public class CreateReviewRequest
    {
        public int? ServiceId { get; set; }
        public int? ListingId { get; set; }
        [Required]
        public int Stars { get; set; }
        [Required]
        public string Body { get; set; }
        [Required]
        public string Title { get; set; }
    }
}
