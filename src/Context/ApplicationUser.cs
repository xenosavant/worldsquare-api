using Microsoft.AspNetCore.Identity;
using Stellmart.Api.Context.Entities;
using Stellmart.Api.Context.Entities.Entity;
using Stellmart.Api.Context.Entities.ReadOnly;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Linq;

namespace Stellmart.Api.Context
{
    public class ApplicationUser : IdentityUser<int>, IAuditableEntity, IEntityMaximum
    {

        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        public string IpAddress { get; set; }

        public bool MustRecoverKey { get; set; }

        public bool MustResetKey { get; set; }

        public string StellarPublicKey { get; set; }

        public byte [] StellarEncryptedSecretKey { get; set; }

        public byte [] StellarRecoveryKey { get; set; }

        public byte[] StellarSecretKeyIv { get; set; }

        public bool ManagedAccount { get; set; }

        public int? RewardsLevelId { get; set; }

        public int? TwoFactorTypeId { get; set; }

        public int? NativeCurrencyId { get; set; }

        public int? VerificationLevelId { get; set; }
        public int? CountryId { get; set; }

        public bool Flagged { get; set; }

        public bool UseTwoFactorForLogin { get; set; }

        public string TotpSecret { get; set; }

        public string TwoFactorCode { get; set; }

        public string SecurityQuestions { get; set; }

        public int TwoFactorFailedCount { get; set; }

        [DefaultValue(5)]
        public int MaxTwoFactorFailedAccessAttempts { get; set; }

        [DefaultValue(5)]
        public int DefaultTwoFatorLockoutMinutes{ get; set; }

        public virtual ICollection<Location> Locations { get; set; }

        public virtual RewardsLevel RewardsLevel { get; set; }

        public virtual VerificationLevel VerificationLevel { get; set; }

        public virtual TwoFactorAuthenticationType TwoFactorAuthenticationType { get; set; }

        public virtual Currency NativeCurrency { get; set; }

        [ForeignKey("UserId")]
        public virtual ICollection<OnlineStore> OnlineStores { get; set; }

        [ForeignKey("CountryId")]
        public virtual Country Country { get; set; }

        [ForeignKey("UserId")]
        public virtual ICollection<DeliveryService> DeliveryServices { get; set; }

        [ForeignKey("UserId")]
        public virtual ICollection<DistributionCenter> DistributionCenters { get; set; }

        [ForeignKey("UserId")]
        public virtual ICollection<RideService> RideServices { get; set; }

        [ForeignKey("RequestorId")]
        public virtual ICollection<DeliveryRequest> DeliveryRequests { get; set; }

        [ForeignKey("RequestorId")]
        public virtual ICollection<RideRequest> RideRequests { get; set; }

        [ForeignKey("SignerId")]
        public virtual ICollection<UserSignature> Signatures { get; set; }

        public virtual ICollection<TradeItem> TradeItems { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

        public virtual ICollection<ProductShipment> SentShipments { get; set; }

        public virtual ICollection<ProductShipment> ReceivedShipments { get; set; }

        public virtual ICollection<BuyerSecretKey> BuyerSecretKeys { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<KycData> KycDatas { get; set; }

        public virtual ICollection<MessageThreadMember> MessageThreadMembers { get; set; }

        [NotMapped]
        public IEnumerable<MessageThread> Threads => MessageThreadMembers?.Select(t => t.Thread);

        // IEntity

        public Guid UniqueId { get; set; }

        public bool IsDeleted { get; set; }

        private DateTime? _createdDate;
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate
        {
            get => _createdDate ?? DateTime.UtcNow;
            set => _createdDate = value;
        }

        public int CreatedBy { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedBy { get; set; }

        object IEntity.Id
        {
            get { return this.Id; }
        }

        public bool IsActive { get; set; }
    }
}