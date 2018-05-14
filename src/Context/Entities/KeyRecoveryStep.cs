using Stellmart.Api.Context.Entities.BaseEntity;

namespace Stellmart.Context.Entities
{
    public class KeyRecoveryStep : Entity<int>
    {

        public int OrderNumber { get; set; }

        public int UserId { get; set; }

        public int SecurityQuestionId { get; set; }

        public SecurityQuestion SecurityQuestion { get; set; }
    }
}
