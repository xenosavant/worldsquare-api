using System.Collections.Generic;

namespace Stellmart.Api.Data.Account
{
    public class ApplicationUserModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public IReadOnlyCollection<string> Questions { get; set; }
        public IReadOnlyCollection<string> Answers { get; set; }
        public string IpAddress { get; set; }
        public string StellarPublicKey { get; set; }
        public byte[] StellarEncryptedSecretKey { get; set; }
        public byte[] StellarRecoveryKey { get; set; }
        public byte[] StellarSecretKeyIv { get; set; }
        public int? PrimaryShippingLocationId { get; set; }

        public int? RewardsLevelId { get; set; }

        public int? TwoFactorTypeId { get; set; }

        public int? NativeCurrencyId { get; set; }

        public int? VerificationLevelId { get; set; }
        public int CountryId { get; set; }
        public string SecurityQuestions { get; set; }
    }
}
