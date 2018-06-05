using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities
{
    public class OracleSignature : Signature
    {
        [Required]
        public string OracleId { get; set; }

    }
}
