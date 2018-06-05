using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Stellmart.Api.Context;
using Stellmart.Api.Context.Entities;
using Stellmart.Api.Context.Entities.ReadOnly;

namespace Stellmart.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        // Entities

        public DbSet<Area> Areas { get; set; }

        public DbSet<Contract> Contracts { get; set; }

        public DbSet<ContractPhase> ContractPhases { get; set; }

        public DbSet<CurrencyAmount> CurrencyAmounts { get; set; }

        public DbSet<DisputeTransaction> DisputeTransactions { get; set; }

        public DbSet<DistributionCenter> DistributionCenters { get; set; }

        public DbSet<GeoLocation> GeoLocations { get; set; }

        public DbSet<InventoryItem> InventoryItems { get; set; }

        public DbSet<LineItem> LineItems { get; set; }

        public DbSet<Listing> Listings { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<MessageThread> MessageThreads { get; set; }

        public DbSet<ReleaseTransaction> ReleaseTransaction { get; set; }

        public DbSet<DisputeTransaction> DisputeTransaction { get; set; }

        public DbSet<TimeOverrideTransaction> TimeOverrideTransaction { get; set; }

        public DbSet<OracleBumpTransaction> OracleBumpTransaction { get; set; }

        public DbSet<PricePerDistance> PricePerDistances { get; set; }

        public DbSet<PricePerTime> PricePerTimes { get; set; }

        public DbSet<ProductShipment> ProductShipments { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<OnlineStore> OnlineStore { get; set; }

        public DbSet<DeliveryService> DeliveryService { get; set; }

        public DbSet<DeliveryRequest> DeliveryRequest { get; set; }

        public DbSet<ServiceRequestFulfillment> ServiceRequestFulfillment { get; set; }

        public DbSet<ShippingManifest> ShippingManifests { get; set; }

        public DbSet<OracleSignature> OracleSignatures { get; set; }

        public DbSet<SystemSignature> SystemSignature { get; set; }

        public DbSet<UserSignature> UserSignature { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        // Read Only Data

        public DbSet<Condition> Conditions { get; set; }

        public DbSet<ContractState> ContractStates { get; set; }

        public DbSet<ContractType> CobntractTypes { get; set; }

        public DbSet<Currency> Currencies { get; set; }

        public DbSet<DistanceUnit> DistanceUnits { get; set; }

        public DbSet<FulfillmentState> FulfillmentStates { get; set; }

        public DbSet<ListingCategory> ListingCategory { get; set; }

        public DbSet<PretransactionType> PretransactionTypes { get; set; }

        public DbSet<QuantityUnit> QuantityUnits { get; set; }

        public DbSet<RewardsLevel> RewardsLevels { get; set; }

        public DbSet<ShippingCarrier> ShippingCarriers { get; set; }

        public DbSet<SubCategory> SubCategories { get; set; }

        public DbSet<SuperCategory> SuperCategories { get; set; }

        public DbSet<TimeUnit> TimeUnits { get; set; }

        public DbSet<TradeInState> TradeInStates { get; set; }

        public DbSet<TwoFactorAuthenticationType> TwoFactorAuthenticationTypes { get; set; }

        public DbSet<VerificationLevel> VerificationLevels { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ShippingManifestLineItem>()
                .HasKey(t => new { t.ShippingManifestId, t.LineItemId });

            modelBuilder.Entity<ShippingManifestLineItem>()
                .HasOne(smli => smli.LineItem)
                .WithMany(li => li.ShippingManifestLineItems)
                .HasForeignKey(smli => smli.LineItemId);

            modelBuilder.Entity<ShippingManifestLineItem>()
                .HasOne(smli => smli.Manifest)
                .WithMany(sm => sm.ShippingManifestLineItems)
                .HasForeignKey(smli => smli.ShippingManifestId);
        }
    }
}
