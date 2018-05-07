using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stellmart.Context.Entities
{
    public class User : IStellmartEntity
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(25)]
        [Required]
        public string Username { get; set; }

        [MaxLength(256)]
        [Required]
        public string Email { get; set; }

        public bool Verified { get; set; }

        public bool MustRecoverKey { get; set; }

        public bool MustResetPassword { get; set; }

        public string StellarPublicKey { get; set;  }

        public string StellarPrivateKey { get; set; }

        public string StellarRecoveryKey { get; set; }

        public ICollection<KeyRecoveryStep> KeyRecoverySteps { get; set; }
    }
}
