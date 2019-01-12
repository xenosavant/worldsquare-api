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

        public Obligation Obligation => Obligations.FirstOrDefault();

        public override void Update(IContractService contractService)
        {
            foreach (var contract in Obligation.Contracts)
            {
                // update contracts
            }
        }
    }
}
