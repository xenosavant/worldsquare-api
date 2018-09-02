using System.Collections.Generic;

namespace Stellmart.Api.Data.Account
{
    public class ApplicationUserModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public IReadOnlyCollection<string> Questions { get; set; }
        public IReadOnlyCollection<string> Answers { get; set; }
    }
}
