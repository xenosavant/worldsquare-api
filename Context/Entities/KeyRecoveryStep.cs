using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Context.Entities
{
    public class KeyRecoveryStep : IStellmartEntity
    {
        [Key]
        public int Id { get; set; }

        public int OrderNumber { get; set; }

        public int UserId { get; set; }

        public int SecurityQuestionId { get; set; }

        public SecurityQuestion SecurityQuestion { get; set; }

        public User User { get; set; }
    }
}
