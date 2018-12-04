using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Data.ViewModels
{
    public class ReviewViewModel
    {
        public int ServiceId { get; set; }
        public int Id { get; set; }
        public int Stars { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
}
