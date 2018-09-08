using System.Collections.Generic;

namespace Stellmart.Data.Account
{
    public class SignupRequest
    {
        public string Email { get; set;  }
        public string Password { get; set; }
        public IReadOnlyCollection<string> SecurityQuestions { get; set; }
        public IReadOnlyCollection<string> SecurityAnswers { get; set; }

    }
}
