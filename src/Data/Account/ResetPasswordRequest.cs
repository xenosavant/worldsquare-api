namespace Stellmart.Api.Data.Account
{
    /// <summary>
    /// Reset password model - represents input for reset password form for user
    /// </summary>
    public class ResetPasswordRequest
    {
        /// <summary>
        /// Represents id of user to reset password
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// Represents reset password form's email address input
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Represents reset password form's confirm email input
        /// </summary>
        public string ConfirmPassword { get; set; }
        /// <summary>
        /// Represents reset password code
        /// </summary>
        public string Code { get; set; }
    }
}
