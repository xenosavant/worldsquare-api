using System;

namespace Stellmart.Api.Data.Kyc
{
    public class KycProfileModel
    {
        public string UserIdentifier { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string AddressLine4 { get; set; }
        public string AddressLine5 { get; set; }
        public string AddressLine6 { get; set; }
        public string Gender { get; set; }
        public string Nationality { get; set; }
        public int UserId { get; set; }
    }
}
