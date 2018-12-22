using Stellmart.Api.Context.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities
{
    public class Obligation : UniqueEntity
    {
        public int ServiceId { get; set; }
        public int? ProviderId { get; set; }
        public int RecipientId { get; set; }
        public int? ArbiterId { get; set; }
        public DateTime ServiceInitiationTimeLimit { get; set; }
        public DateTime ServiceFulfillmentTimeLimit { get; set; }
        public DateTime ServiceReceiptTimeLimit { get; set; }
        public int IntermediaryPhases { get; set; }
        public bool Fulfilled { get; set; }
        public ICollection<Contract> ServiceContracts { get; set; }
        public Contract CollateralContract { get; set; }
    }
}
