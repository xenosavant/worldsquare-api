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

        public DbSet<PreTransaction> PreTransactions { get; set; }

        public DbSet<PricePerDistance> PricePerDistances { get; set; }

        public DbSet<PricePerTime> PricePerTimes { get; set; }

        public DbSet<ProductShipment> ProductShipments { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Service> Services { get; set; }

        public DbSet<OnlineStore> OnlineStores { get; set; }

        public DbSet<RideService> RideServices { get; set; }

        public DbSet<DeliveryService> DeliveryServices { get; set; }

        public DbSet<ServiceRequest> ServiceRequests { get; set; }

        public DbSet<DeliveryRequest> DeliveryRequests { get; set; }

        public DbSet<RideRequest> RideRequests { get; set; }

        public DbSet<ServiceRequestFulfillment> ServiceRequestFulfillments { get; set; }

        public DbSet<DeliveryRequestFulfillment> DeliveryRequestFulfillments { get; set; }

        public DbSet<ShippingManifest> ShippingManifests { get; set; }

        public DbSet<TradeItem> TradeItems { get; set; }

        public DbSet<ShippingManifestLineItem> ShippingManifestLineItems { get; set; }

        public DbSet<OnlineStoreReview> OnlineStoreReviews { get; set; }

        public DbSet<Signature> Signatures { get; set; }

        public DbSet<SystemSignature> SystemSignatures { get; set; }

        public DbSet<SystemSignature> OracleSignatures { get; set; }

        public DbSet<SystemSignature> Userignatures { get; set; }

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
                .HasForeignKey(sl => sl.LineItemId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ShippingManifestLineItem>()
                .HasOne(sl => sl.Manifest)
                .WithMany(m => m.ShippingManifestLineItems)
                .HasForeignKey(sl => sl.ShippingManifestId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OnlineStoreReview>()
                .HasKey(or => new { or.OnlineStoreId, or.ReviewId });

            modelBuilder.Entity<OnlineStoreReview>()
                .HasOne(or => or.OnlineStore)
                .WithMany(o => o.OnlineStoreReviews)
                .HasForeignKey(or => or.OnlineStoreId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OnlineStoreReview>()
                .HasOne(or => or.Review)
                .WithMany(r => r.OnlineStoreReviews)
                .HasForeignKey(or => or.ReviewId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ListingInventoryItem>()
                .HasKey(li => new { li.ListingId, li.InventoryItemId });

            modelBuilder.Entity<ListingInventoryItem>()
                .HasOne(li => li.InventoryItem)
                .WithMany(i => i.ListingInventoryItems)
                .HasForeignKey(li => li.InventoryItemId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ListingInventoryItem>()
                .HasOne(li => li.Listing)
                .WithMany(l => l.ListingInventoryItems)
                .HasForeignKey(li => li.ListingId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ServiceRequest>()
                .HasOne(d => d.Destination)
                .WithOne(l => l.ServiceRequestDestination)
                .HasForeignKey<ServiceRequest>(d => d.DestinationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ServiceRequest>()
               .HasOne(d => d.Location)
               .WithOne(l => l.ServiceRequestLocation)
               .HasForeignKey<ServiceRequest>(d => d.LocationId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TradeItem>()
                .HasOne(t => t.TradeInValue)
                .WithOne(l => l.TradeItem)
                .HasForeignKey<TradeItem>(t => t.TradeInValueId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TradeItem>()
                .HasOne(t => t.TradeInState)
                .WithMany(s => s.TradeItems)
                .HasForeignKey(t => t.TradeInStateId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TradeItem>()
                .HasOne(t => t.ItemMetaData)
                .WithOne(i => i.TradeItem)
                .HasForeignKey<TradeItem>(t => t.ItemMetaDataId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TradeItem>()
                .HasOne(t => t.Owner)
                .WithMany(o => o.TradeItems)
                .HasForeignKey(t => t.TradeInValueId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ServiceRequestFulfillment>()
                .HasOne(s => s.Service)
                .WithMany(sr => sr.Fulfillments)
                .HasForeignKey(s => s.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ServiceRequestFulfillment>()
                .HasOne(s => s.ServiceRequest)
                .WithMany(sr => sr.Fulfillments)
                .HasForeignKey(s => s.ServiceRequestId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ServiceRequestFulfillment>()
                .HasOne(s => s.Contract)
                .WithOne(c => c.Fulfillment)
                .HasForeignKey<ServiceRequestFulfillment>(t => t.ContractId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ServiceRequestFulfillment>()
                .HasOne(s => s.FulfillmentState)
                .WithMany(f => f.Fulfillments)
                .HasForeignKey(t => t.FulfillmentStateId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Reviewer)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.ReviewerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProductShipment>()
                .HasOne(s => s.Sender)
                .WithMany(u => u.SentShipments)
                .HasForeignKey(s => s.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProductShipment>()
                .HasOne(s => s.Receiver)
                .WithMany(u => u.ReceivedShipments)
                .HasForeignKey(s => s.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProductShipment>()
                .HasOne(s => s.Carrier)
                .WithMany(c => c.Shipments)
                .HasForeignKey(s => s.ShippingCarrierId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProductShipment>()
                .HasOne(s => s.DeliveryRequest)
                .WithOne(d => d.Shipment)
                .HasForeignKey<ProductShipment>(s => s.DeliveryRequestId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DeliveryRequest>()
                .HasOne(d => d.Requestor)
                .WithMany(d => d.DeliveryRequests)
                .HasForeignKey(s => s.RequestorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RideRequest>()
                .HasOne(d => d.Requestor)
                .WithMany(d => d.RideRequests)
                .HasForeignKey(s => s.RequestorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProductShipment>()
                .HasOne(s => s.Manifest)
                .WithOne(m => m.Shipment)
                .HasForeignKey<ProductShipment>(s => s.ShippingManifestId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProductShipment>()
                .HasOne(s => s.Contract)
                .WithOne(c => c.ProductShipment)
                .HasForeignKey<ProductShipment>(s => s.ContractId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MessageThread>()
                .HasOne(s => s.Initiator)
                .WithMany(u => u.Threads)
                .HasForeignKey(s => s.InitiatorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Review>()
                .HasOne(s => s.Reviewer)
                .WithMany(u => u.Reviews)
                .HasForeignKey(s => s.ReviewerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OnlineStore>()
                .HasOne(s => s.Location)
                .WithOne(u => u.OnlineStore)
                .HasForeignKey<OnlineStore>(s => s.LocationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OnlineStore>()
                .HasOne(s => s.ServiceRegion)
                .WithOne(u => u.OnlineStore)
                .HasForeignKey<OnlineStore>(s => s.ServiceRegionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OnlineStore>()
                .HasOne(s => s.User)
                .WithMany(u => u.OnlineStores)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RideService>()
                .HasOne(s => s.User)
                .WithMany(u => u.RideServices)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DeliveryService>()
                .HasOne(s => s.User)
                .WithMany(u => u.DeliveryServices)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DistributionCenter>()
                .HasOne(s => s.User)
                .WithMany(u => u.DistributionCenters)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Service>()
                .HasOne(s => s.NativeCurrency)
                .WithMany(c => c.Services)
                .HasForeignKey(s => s.NativeCurrencyId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ApplicationUser>()
                .HasOne(u => u.NativeCurrency)
                .WithMany(c => c.Users)
                .HasForeignKey(s => s.NativeCurrencyId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ApplicationUser>()
                .HasOne(u => u.PrimaryShippingLocation)
                .WithOne(c => c.User)
                .HasForeignKey<ApplicationUser>(s => s.PrimaryShippingLocationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ApplicationUser>()
                .HasOne(u => u.RewardsLevel)
                .WithMany(c => c.Users)
                .HasForeignKey(s => s.RewardsLevelId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ApplicationUser>()
                .HasOne(u => u.TwoFactorAuthenticationType)
                .WithMany(c => c.Users)
                .HasForeignKey(s => s.TwoFactorTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ApplicationUser>()
                .HasOne(u => u.VerificationLevel)
                .WithMany(c => c.Users)
                .HasForeignKey(s => s.VerificationLevelId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Contract>()
                .HasMany(c => c.Phases)
                .WithOne(p => p.Contract)
                .HasForeignKey(p => p.ContractId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Location>()
                .HasOne(u => u.GeoLocation)
                .WithOne(g => g.Location)
                .HasForeignKey<Location>(l => l.GeoLocationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<InventoryItem>()
                .HasOne(u => u.UnitType)
                .WithMany(i => i.InventoryItems)
                .HasForeignKey(l => l.UnitTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PricePerDistance>()
                .HasOne(u => u.Amount)
                .WithOne(a => a.PricePerDistance)
                .HasForeignKey<PricePerDistance>(l => l.CurrencyAmountId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PricePerDistance>()
                .HasOne(u => u.DistanceUnit)
                .WithMany(d => d.PricePerDistances)
                .HasForeignKey(l => l.DistanceUnitId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PricePerTime>()
               .HasOne(u => u.Amount)
               .WithOne(a => a.PricePerTime)
               .HasForeignKey<PricePerTime>(l => l.CurrencyAmountId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PricePerTime>()
               .HasOne(u => u.TimeUnit)
               .WithMany(d => d.PricePerTimes)
               .HasForeignKey(l => l.TimeUnitId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Region>()
               .HasOne(r => r.Location)
               .WithOne(l => l.Region)
               .HasForeignKey<Region>(r => r.LocationId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ContractPhase>()
                .HasMany(c => c.Transactions)
                .WithOne(t => t.Phase)
                .HasForeignKey(p => p.ContractPhaseId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ItemMetaData>()
                 .HasOne(i => i.ListingCategory)
                 .WithMany(l => l.ItemMetaDatas)
                 .HasForeignKey(r => r.ListingCategoryId)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ItemMetaData>()
                 .HasOne(i => i.SubCategory)
                 .WithMany(s => s.ItemMetaDatas)
                 .HasForeignKey(r => r.SubCategoryId)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ItemMetaData>()
                 .HasOne(r => r.SuperCategory)
                 .WithMany(s => s.ItemMetaDatas)
                 .HasForeignKey(r => r.SuperCategoryId)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ItemMetaData>()
                 .HasOne(r => r.Listing)
                 .WithOne(s => s.ItemMetaData)
                 .HasForeignKey<Listing>(l => l.ItemMetaDataId)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ItemMetaData>()
                 .HasOne(r => r.ItemCondition)
                 .WithMany(s => s.ItemMetaDatas)
                 .HasForeignKey(l => l.ItemConditionId)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LineItem>()
                 .HasOne(l => l.InventoryItem)
                 .WithOne(i => i.LineItem)
                 .HasForeignKey<LineItem>(l => l.InventoryItemId)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Area>()
                 .HasOne(a => a.GeoLocation)
                 .WithOne(g => g.Area)
                 .HasForeignKey<Area>(a => a.GeoLocationId)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Signature>()
                 .HasOne(s => s.Transaction)
                 .WithMany(t => t.Signatures)
                 .HasForeignKey(s => s.PreTransactionId)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserSignature>()
                 .HasOne(s => s.Signer)
                 .WithMany(u => u.Signatures)
                 .HasForeignKey(s => s.SignerId)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
                 .HasOne(m => m.Poster)
                 .WithMany(u => u.Messages)
                 .HasForeignKey(s => s.PosterId)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
                 .HasOne(m => m.MessageThread)
                 .WithMany(t => t.Messages)
                 .HasForeignKey(m => m.MessageThreadId)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Listing>()
                 .HasOne(l => l.OnlineStore)
                 .WithMany(u => u.Listings)
                 .HasForeignKey(l => l.ServiceId)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Contract>()
                .HasOne(c => c.State)
                .WithMany(u => u.Contracts)
                .HasForeignKey(c => c.ContractStateId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Contract>()
                .HasOne(c => c.Type)
                .WithMany(u => u.Contracts)
                .HasForeignKey(c => c.ContractTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CurrencyAmount>()
                .HasOne(c => c.CurrencyType)
                .WithMany(a => a.CurrencyAmounts)
                .HasForeignKey(s => s.CurrencyTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DeliveryService>()
                .HasOne(d => d.ServiceArea)
                .WithMany(a => a.DeliveryServices)
                .HasForeignKey(d => d.ServiceAreaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
