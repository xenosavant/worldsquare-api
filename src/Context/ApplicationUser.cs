using Microsoft.AspNetCore.Identity;
using Stellmart.Api.Context.Entities;
using Stellmart.Api.Context.Entities.ReadOnly;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Stellmart.Api.Context
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(25)]
        [Required]
        public string Username { get; set; }

        public bool Verified { get; set; }

        public bool MustRecoverKey { get; set; }

        public bool MustResetKey { get; set; }

        public string StellarPublicKey { get; set; }

        public string StellarPrivateKey { get; set; }

        public bool ManagedAccount { get; set; }

        public int PrimaryShippingLocationId { get; set; }

        public int RewardsLevelId { get; set; }

        public int TwoFactorTypeId { get; set; }

        public int NativeCurrencyId { get; set; }

        public int VerificationLevelId { get; set; }

        public virtual Location PrimaryShippingLocation { get; set; }

        public virtual RewardsLevel RewardsLevel { get; set; }

        public virtual Currency NativeCurrency { get; set; }

        public virtual ICollection<OnlineStore> OnlineStores { get; set; }

        public virtual ICollection<DeliveryService> DeliveryServices { get; set; }

    }
}
