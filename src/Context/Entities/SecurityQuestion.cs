using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Context.Entities
{
    public class SecurityQuestion : IStellmartEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Question { get; set; }
    }
}
