using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Data.Contract
{
    public class GetSignatureModel
    {
        public int ContractId { get; set; }
        public long PhaseNumber { get; set; }
        public int UserId { get; set; }
    }
}
