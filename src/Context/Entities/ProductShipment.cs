using Stellmart.Api.Context.Entities.Entity;
using Stellmart.Api.Context.Entities.ReadOnly;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stellmart.Api.Context.Entities
{
    public class ProductShipment : AuditableEntity<int>
    {
        public int? SenderId { get; set; }

        public int? ReceiverId { get; set; }

        public DateTime OrderDate { get; set; }

        [Required]
        public bool FulfilledInternally { get; set; }

        [Required]
        public bool Internal { get; set; }

        public bool TradeIn { get; set; }

        public int? ShippingCarrierId { get; set; }

        public int? DeliveryRequestId { get; set; }

        public int ShippingManifestId { get; set; }

        public int OrderId { get; set; }

        public int ContractId { get; set; }

        [Required]
        public int FulfillmentStateId { get; set; }

        private ICollection<TradeProductShipment> TradeProductShipments { get; set; }

        [ForeignKey("SenderId")]
        public virtual ApplicationUser Sender { get; set; }

        [ForeignKey("ReceiverId")]
        public virtual ApplicationUser Receiver { get; set; }

        public virtual ShippingCarrier Carrier { get; set; }

        public virtual DeliveryRequest DeliveryRequest { get; set; }

        public virtual ShippingManifest Manifest { get; set; }

        public virtual Order Order { get; set; }

        public virtual Trade Trade { get; set; }

        public virtual Contract Contract { get; set; }

    }
}
