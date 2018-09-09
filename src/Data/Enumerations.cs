using System.ComponentModel;

namespace Stellmart.Api.Data.Enumerations
{
    public enum HttpResponseCodes
    {
        Unauthorized = 401
    }

    public enum TwoFactorTypes
    {
        None = 0,
        Email = 1,
        Sms = 2,
        Totp = 3
    }

    public enum VerificationLevelTypes
    {
        Unverified = 1,
        LevelOne = 2,
        LevelTwo = 3
    }

    public enum RolesTypes
    {
        [Description("member")]
        Member = 1
    }

    public enum PrimaryShippingLocationTypes
    {
        Default = 1
    }

    public enum RewardsLevelTypes
    {
        Default = 1
    }

    public enum NativeCurrencyTypes
    {
        Default = 1
    }
}
