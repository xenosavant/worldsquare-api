using Microsoft.AspNetCore.Identity;
using Stellmart.Api.Context.Entities;
using Stellmart.Api.Context.Entities.Entity;
using Stellmart.Api.Context.Entities.ReadOnly;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace Stellmart.Api.Context
{
    public class ApplicationUser : IdentityUser<int>, IAuditableEntity, IEntityMaximum
    {

        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

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

        public bool Flagged { get; set; }

        public virtual Location PrimaryShippingLocation { get; set; }

        public virtual RewardsLevel RewardsLevel { get; set; }

        public virtual Currency NativeCurrency { get; set; }

        public virtual ICollection<OnlineStore> OnlineStores { get; set; }

        public virtual ICollection<DeliveryService> DeliveryServices { get; set; }

        public Guid UniqueId { get; set; }

        public bool IsDeleted { get; set; }

        private DateTime? _createdDate;
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate
        {
            get => _createdDate ?? DateTime.UtcNow;
            set => _createdDate = value;
        }

        public string CreatedBy { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }

        object IEntity.Id
        {
            get { return this.Id; }
        }

        public bool IsActive { get; set; }
    }
}
