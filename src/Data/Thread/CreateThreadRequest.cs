using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Data.Thread
{
    public class CreateThreadRequest
    {
        public int ListingId { get; set; }
        public string Message { get; set; }
    }
}
