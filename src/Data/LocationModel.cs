namespace Stellmart.Api.Data
{
    public class LocationModel
    {
        public string Address { get; set; }
        public string LocationComponentsFromApp { get; set; }
        public string LocationComponentsFromGoogleApi { get; set; }
        public bool IsActive { get; set; }
    }
}
