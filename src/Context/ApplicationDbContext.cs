using Microsoft.ApplicationInsights;
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

        // Inheritance

        public DbSet<Interaction> Interactions { get; set; }
        public DbSet<OnlineSale> OnlineSales { get; set; }
        public DbSet<Trade> Trades { get; set; }
        public DbSet<ServiceFulfillment> ServiceFulfillments { get; set; }

        public DbSet<Service> Services { get; set; }
        public DbSet<RideService> RideServices { get; set; }
        public DbSet<OnlineStore> OnlineStores { get; set; }
        public DbSet<DistributionCenter> DistributionCenters { get; set; }

        
        public DbSet<Signature> Signatures { get; set; }
        public DbSet<UserSignature> UserSignatures { get; set; }
        public DbSet<SystemSignature> SystemSignatures { get; set; }
        public DbSet<SecretSignature> SecretSignatures { get; set; }

        public DbSet<Contract> Contracts { get; set; }

        public DbSet<SecretKey> SecretKeys { get; set; }
        public DbSet<BuyerSecretKey> BuyerSecretKeys { get; set; }

        // Entities

        public DbSet<Area> Areas { get; set; }

        public DbSet<ContractPhase> ContractPhases { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<CurrencyAmount> CurrencyAmounts { get; set; }
        public DbSet<GeoLocation> GeoLocations { get; set; }
        public DbSet<InventoryItem> InventoryItems { get; set; }
        public DbSet<ItemMetaData> ItemMetaDatas { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<ItemMetaDataCategory> ItemMetaDataCategories { get; set; }

        public DbSet<KycData> KycDatas { get; set; }
        public DbSet<LineItem> LineItems { get; set; }
        public DbSet<Listing> Listings { get; set; }
        public DbSet<Location> Locations { get; set; }

        public DbSet<Message> Messages { get; set; }
        public DbSet<MessageThread> MessageThreads { get; set; }
        public DbSet<PreTransaction> PreTransactions { get; set; }
        public DbSet<PricePerDistance> PricePerDistances { get; set; }
        public DbSet<PricePerTime> PricePerTimes { get; set; }
        public DbSet<ProductShipment> ProductShipments { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public DbSet<ShipmentTracker> ShipmentTrackers { get; set; }
        public DbSet<DeliveryService> DeliveryServices { get; set; }
        public DbSet<ServiceRequest> ServiceRequests { get; set; }
        public DbSet<DeliveryRequest> DeliveryRequests { get; set; }
        public DbSet<RideRequest> RideRequests { get; set; }
        public DbSet<ServiceRequestFulfillment> ServiceRequestFulfillments { get; set; }
        public DbSet<DeliveryRequestFulfillment> DeliveryRequestFulfillments { get; set; }
        public DbSet<TradeItem> TradeItems { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        // Read Only Data

        public DbSet<Category> Categories { get; set; }
        public DbSet<ItemCondition> ItemConditions { get; set; }
        public DbSet<ContractState> ContractStates { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<DistanceUnit> DistanceUnits { get; set; }
        public DbSet<FulfillmentState> FulfillmentStates { get; set; }
        public DbSet<QuantityUnit> QuantityUnits { get; set; }
        public DbSet<RewardsLevel> RewardsLevels { get; set; }
        public DbSet<TimeUnit> TimeUnits { get; set; }
        public DbSet<TradeInState> TradeInStates { get; set; }
        public DbSet<TwoFactorAuthenticationType> TwoFactorAuthenticationTypes { get; set; }
        public DbSet<VerificationLevel> VerificationLevels { get; set; }
        public DbSet<SecurityQuestion> SecurityQuestions { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Properties

            modelBuilder.Entity<CurrencyAmount>(entity =>
            {
                entity.Property(p => p.Amount)
                   .HasColumnType("decimal(14, 2)");
            });

            //  Many to Many relationships

            modelBuilder.Entity<ItemMetaDataCategory>()
                .HasKey(i => new { i.ItemMetaDataId, i.CategoryId });

            modelBuilder.Entity<ItemMetaDataCategory>()
                .HasOne(i => i.Category)
                .WithMany("ItemMetaDataCategories")
                .HasForeignKey(i => i.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ItemMetaDataCategory>()
                .HasOne(i => i.ItemMetaData)
                .WithMany("ItemMetaDataCategories")
                .HasForeignKey(i => i.ItemMetaDataId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TradeProductShipment>()
                .HasKey(t => new { t.TradeId, t.ProductShipmentId });

            modelBuilder.Entity<TradeProductShipment>()
                .HasOne(t => t.Trade)
                .WithMany("TradeProductShipments")
                .HasForeignKey(t => t.TradeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TradeProductShipment>()
               .HasOne(t => t.ProductShipment)
               .WithMany("TradeProductShipments")
               .HasForeignKey(t => t.ProductShipmentId)
               .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<MessageThreadMember>()
               .HasKey(t => new { t.MessageThreadId, t.UserId });

            modelBuilder.Entity<MessageThreadMember>()
                .HasOne(t => t.Thread)
                .WithMany("MessageThreadMembers")
                .HasForeignKey(t => t.MessageThreadId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MessageThreadMember>()
               .HasOne(t => t.User)
               .WithMany("MessageThreadMembers")
               .HasForeignKey(t => t.UserId)
               .OnDelete(DeleteBehavior.Restrict);


            // One to one relationships

            modelBuilder.Entity<OrderItem>()
               .HasOne(o => o.Contract)
               .WithOne(c => c.OrderItem)
               .HasForeignKey<OrderItem>(o => o.ContractId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OnlineSale>()
                .HasOne(os => os.Order)
                .WithOne(o => o.Sale)
                .HasForeignKey<Order>(o => o.OnlineSaleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ServiceFulfillment>()
                .HasOne(s => s.ServiceRequestFulfillment)
                .WithOne(s => s.ServiceFulfillment)
                .HasForeignKey<ServiceFulfillment>(s => s.ServiceRequestFulfillmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // One to many relationships

            modelBuilder.Entity<Review>()
              .HasOne(r => r.Service)
              .WithMany(s => s.Reviews)
              .HasForeignKey(r => r.ServiceId)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Review>()
              .HasOne(r => r.Listing)
              .WithMany(s => s.Reviews)
              .HasForeignKey(r => r.ListingId)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderItem>()
               .HasOne(o => o.InventoryItem)
               .WithMany(ii => ii.OrderItems)
               .HasForeignKey(o => o.InventoryItemId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
               .HasOne(o => o.Purchaser)
               .WithMany(u => u.Orders)
               .HasForeignKey(o => o.PurchaserId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderItem>()
               .HasOne(i => i.TradeItem)
               .WithMany(t => t.OrderItems)
               .HasForeignKey(i => i.TradeItemId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderItem>()
               .HasOne(i => i.Order)
               .WithMany(o => o.Items)
               .HasForeignKey(i => i.OrderId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderItem>()
               .HasOne(i => i.Shipment)
               .WithMany(s => s.Items)
               .HasForeignKey(i => i.ProductShipmentId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BuyerSecretKey>()
               .HasOne(p => p.Buyer)
               .WithMany(o => o.BuyerSecretKeys)
               .HasForeignKey(p => p.UserId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProductShipment>()
                .HasOne(p => p.Order)
                .WithMany(o => o.Shipments)
                .HasForeignKey(p => p.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<InventoryItem>()
                .HasOne(i => i.Listing)
                .WithMany(r => r.InventoryItems)
                .HasForeignKey(i => i.ListingId)
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
                .HasForeignKey(t => t.OwnerId)
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
                .HasForeignKey(r => r.CreatedBy)
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
                .HasOne(s => s.DeliveryRequest)
                .WithOne(d => d.Shipment)
                .HasForeignKey<ProductShipment>(s => s.DeliveryRequestId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OnlineStore>()
                .HasOne(s => s.ServiceRegion)
                .WithOne(u => u.OnlineStore)
                .HasForeignKey<OnlineStore>(s => s.ServiceRegionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RideRequest>()
                .HasOne(s => s.Requestor)
                .WithMany(u => u.RideRequests)
                .HasForeignKey(s => s.RequestorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DeliveryRequest>()
                .HasOne(s => s.Requestor)
                .WithMany(u => u.DeliveryRequests)
                .HasForeignKey(s => s.RequestorId)
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

            modelBuilder.Entity<Listing>()
                .HasOne(u => u.UnitType)
                .WithMany(i => i.Listings)
                .HasForeignKey(l => l.UnitTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TradeItem>()
                .HasOne(t => t.TradeInValue)
                .WithOne(c => c.TradeItem)
                .HasForeignKey<TradeItem>(t => t.ValueId)
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

            modelBuilder.Entity<ContractPhase>()
                .HasMany(c => c.Transactions)
                .WithOne(t => t.Phase)
                .HasForeignKey(p => p.ContractPhaseId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Category>()
                .HasOne(c => c.ParentCategory)
                .WithMany(p => p.ChildCategories)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ItemMetaData>()
                 .HasOne(r => r.Listing)
                 .WithOne(s => s.ItemMetaData)
                 .HasForeignKey<Listing>(l => l.ItemMetaDataId)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ItemMetaData>()
                 .HasOne(r => r.TradeItem)
                 .WithOne(s => s.ItemMetaData)
                 .HasForeignKey<TradeItem>(l => l.ItemMetaDataId)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ItemMetaData>()
                 .HasOne(r => r.ItemCondition)
                 .WithMany(s => s.ItemMetaDatas)
                 .HasForeignKey(l => l.ItemConditionId)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LineItem>()
                 .HasOne(l => l.InventoryItem)
                 .WithMany(i => i.LineItems)
                 .HasForeignKey(l => l.InventoryItemId)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LineItem>()
                 .HasOne(l => l.Cart)
                 .WithMany(i => i.LineItems)
                 .HasForeignKey(l => l.CartId)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Area>()
                 .HasOne(a => a.GeoLocation)
                 .WithOne(g => g.Area)
                 .HasForeignKey<Area>(a => a.GeoLocationId)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ShipmentTracker>()
                 .HasOne(p => p.Signature)
                 .WithOne(t => t.Tracker)
                 .HasForeignKey<ShipmentTracker>(t => t.SignatureId)
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
                 .HasForeignKey(s => s.CreatedBy)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
                 .HasOne(m => m.MessageThread)
                 .WithMany(t => t.Messages)
                 .HasForeignKey(m => m.MessageThreadId)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MessageThread>()
                 .HasOne(m => m.Initiator)
                 .WithMany(u => u.Threads)
                 .HasForeignKey(m => m.CreatedBy)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Listing>()
                 .HasOne(l => l.OnlineStore)
                 .WithMany(u => u.Listings)
                 .HasForeignKey(l => l.ServiceId)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Listing>()
                 .HasOne(l => l.Currency)
                 .WithMany(u => u.Listings)
                 .HasForeignKey(l => l.CurrencyId)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Contract>()
                .HasOne(c => c.Obligation)
                .WithMany(u => u.Contracts)
                .HasForeignKey(c => c.ObligationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Contract>()
                .HasOne(c => c.State)
                .WithMany(u => u.Contracts)
                .HasForeignKey(c => c.ContractStateId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Obligation>()
                .HasOne(c => c.Interaction)
                .WithMany(u => u.Obligations)
                .HasForeignKey(c => c.InteracationId)
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
