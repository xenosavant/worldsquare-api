namespace Stellmart.Api.Data
{
    public class LocationModel
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string LocationComponentsFromApp { get; set; }
        public string LocationComponentsFromGoogleApi { get; set; }
        public double Latitude { get; set; }
        public double Longtitude { get; set; }
        public bool IsActive { get; set; }
        public bool IsDefault { get; set; }
    }
}
