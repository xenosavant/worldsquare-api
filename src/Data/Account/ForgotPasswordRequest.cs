namespace Stellmart.Api.Data.Account
{
    /// <summary>
    /// Forgot password model - represents input for forgot passsword form for user
    /// </summary>
    public class ForgotPasswordRequest
    {
        /// <summary>
        /// Represents forgot password form's email address input
        /// </summary>
        public string Email { get; set; }
    }
}
