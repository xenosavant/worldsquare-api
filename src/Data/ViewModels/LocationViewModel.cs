using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Data.ViewModels
{
    public class LocationViewModel
    {
        public int Id { get; set; }

        public string Address { get; set; }

        public int GeoLocationId { get; set; }

        public string LocationComponents { get; set; }

        public string PlaceId { get; set; }

        public bool Verified { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set;  }
    }
}
