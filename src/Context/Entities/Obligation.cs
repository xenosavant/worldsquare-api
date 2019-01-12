using Stellmart.Api.Context.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities
{
    public class Obligation : UniqueEntity
    {
        // the service
        public int ServiceId { get; set; }
        // the user providing the service (null means ws)
        public int? ProviderId { get; set; }
        // the recipient of the service
        public int RecipientId { get; set; }
        // the party arbitrating the transactions (third signer) (null means ws)
        public int? ArbiterId { get; set; }
        // the interaction that the obligation is part of
        public int InteracationId { get; set; }
        // The time limit in the initial phase after funding in which the provider 
        // must initiate the service. For an online sale, this would mean shipping all of the products.
        // For a ride service, this would mean picking up the rider.
        // If the service is not initiated within this time,  
        // then the refund transaction will be submitted and funds returned to the recipient
        public DateTime ServiceInitiationTimeLimit { get; set; }
        // The maximum time that can elapse before a service is completed and the contract transitions 
        // to the receipt phase. After this time, if the service is not in the receipt phase,
        // it will move to the dispute phase
        public DateTime ServiceFulfillmentTimeLimit { get; set; }
        // The maximum time that can elapse before a service is marked as successful or 
        // contested by the recipient.This will generally be ServiceFulfillmentTimeLimit + N,
        // where N is some time window.This will ensure that the recipient has some minimum amount 
        // of time to either accept or contest the successful receipt of the service.
        public DateTime ServiceReceiptTimeLimit { get; set; }
        // the number of intermediary phases between the initialization phase and the receipt phase. 
        // A service like a single ride would have no intermediary phases, 
        // while a delivery would likely have one intermediary phase,
        // which would be the delivery of the product
        public int IntermediaryPhases { get; set; }
        public bool Fulfilled { get; set; }
        public ICollection<Contract> Contracts { get; set; }
        public virtual Interaction Interaction { get; set; }

    }
}
