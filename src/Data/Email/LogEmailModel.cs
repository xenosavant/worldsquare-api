using Stellmart.Api.Data.Enums;

namespace Stellmart.Api.Data.Email
{
    public class LogEmailModel
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public EmailMessageTypes Type { get; set; }
        public string From { get; set; }
    }
}
