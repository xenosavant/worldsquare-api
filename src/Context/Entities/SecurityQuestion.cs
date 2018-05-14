using Stellmart.Api.Context.Entities.BaseEntity;
using System.ComponentModel.DataAnnotations;

namespace Stellmart.Context.Entities
{
    public class SecurityQuestion : Entity<int>
    {

        [Required]
        public string Question { get; set; }
    }
}
