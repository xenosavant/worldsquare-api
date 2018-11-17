namespace Stellmart.Api.Data.Account
{
    /// <summary>
    /// Confirm email model - represents input for confirm email
    /// </summary>
    public class ConfirmEmailRequest
    {
        /// <summary>
        /// Represents confirmation code for email
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Represents id of user to confirm email
        /// </summary>
        public string UserId { get; set; }
    }
}
