using Stellmart.Api.Context.Entities.Entity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stellmart.Api.Context.Entities
{
    public class KycData : Entity<int>
    {
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
        public string Gender { get; set; }
        public string Nationality { get; set; }
        [ForeignKey("CountryId")]
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
