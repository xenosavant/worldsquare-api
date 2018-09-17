using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Stellmart.Api.Services.Interfaces;

namespace Stellmart.Api.Context.Entities
{
    public class Trade : Interaction
    {
        private ICollection<TradeProductShipment> TradeProductShipments { get; set; }

        [NotMapped]
        public IEnumerable<ProductShipment> Shipments => TradeProductShipments?.Select(t => t.ProductShipment);

        public override void Update(IContractService contractService)
        {
            // Iterate through product shipemts 
        }
    }
}
