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

        public DbSet<ItemMetaData> ItemMetaDatas { get; set; }

        public DbSet<LineItem> LineItems { get; set; }

        public DbSet<Listing> Listings { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<ListingInventoryItem> ListingInventoryItems { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<MessageThread> MessageThreads { get; set; }

        public DbSet<DisputeTransaction> DisputeTransaction { get; set; }

        public DbSet<PreTransaction> PreTransactions { get; set; }

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

        public DbSet<TradeItem> TradeItems { get; set; }

        public DbSet<ShippingManifestLineItem> ShippingManifestLineItems { get; set; }

        public DbSet<OnlineStoreReview> OnlineStoreReviews { get; set; }

        public DbSet<OracleSignature> OracleSignatures { get; set; }

        public DbSet<SystemSignature> SystemSignature { get; set; }

        public DbSet<UserSignature> UserSignature { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        // Read Only Data

        public DbSet<ItemCondition> ItemConditions { get; set; }

        public DbSet<ContractState> ContractStates { get; set; }

        public DbSet<ContractType> ContractTypes { get; set; }

        public DbSet<Currency> Currencies { get; set; }

        public DbSet<DistanceUnit> DistanceUnits { get; set; }

        public DbSet<FulfillmentState> FulfillmentStates { get; set; }

        public DbSet<ListingCategory> ListingCategory { get; set; }

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

            //  Many to Many relationships

            modelBuilder.Entity<ShippingManifestLineItem>()
                .HasKey(sl => new { sl.ShippingManifestId, sl.LineItemId });

            modelBuilder.Entity<ShippingManifestLineItem>()
                .HasOne(sl => sl.LineItem)
                .WithMany(l => l.ShippingManifestLineItems)
                .HasForeignKey(sl => sl.LineItemId);

            modelBuilder.Entity<ShippingManifestLineItem>()
                .HasOne(sl => sl.Manifest)
                .WithMany(m => m.ShippingManifestLineItems)
                .HasForeignKey(sl => sl.ShippingManifestId);

            modelBuilder.Entity<OnlineStoreReview>()
                .HasKey(or => new { or.OnlineStoreId, or.ReviewId });

            modelBuilder.Entity<OnlineStoreReview>()
                .HasOne(or => or.OnlineStore)
                .WithMany(o => o.OnlineStoreReviews)
                .HasForeignKey(or => or.OnlineStoreId);

            modelBuilder.Entity<OnlineStoreReview>()
                .HasOne(or => or.Review)
                .WithMany(r => r.OnlineStoreReviews)
                .HasForeignKey(or => or.ReviewId);

            modelBuilder.Entity<ListingInventoryItem>()
                .HasKey(li => new { li.ListingId, li.InventoryItemId });

            modelBuilder.Entity<ListingInventoryItem>()
                .HasOne(li => li.InventoryItem)
                .WithMany(i => i.ListingInventoryItems)
                .HasForeignKey(li => li.InventoryItemId);

            modelBuilder.Entity<ListingInventoryItem>()
                .HasOne(li => li.Listing)
                .WithMany(l => l.ListingInventoryItems)
                .HasForeignKey(li => li.ListingId);
        }
    }
}
