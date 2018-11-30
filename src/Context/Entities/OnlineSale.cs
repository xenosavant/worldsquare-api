using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Stellmart.Api.Services.Interfaces;

namespace Stellmart.Api.Context.Entities
{
    public class OnlineSale : Interaction
    {
        public virtual Order Order { get; set; }

        public override void Update(IContractService contractService)
        { 
            // update contract for all order items
        }
    }
}
