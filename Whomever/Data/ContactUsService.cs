//Only used to log ui to console
//Interface: in case to actually implement extended mail service usage (with auto ext)

namespace Whomever.Data
{
    public class ContactUsService : IContactUsService
    {
        private readonly ILogger<ContactUsService> _logger;

        public ContactUsService(ILogger<ContactUsService> logger)
        {
            _logger = logger;
        }

        public void SendMessage(string to, string subject, string body)
        {
            //log msg
            _logger.LogInformation($"To: {to} Subject: {subject} Body: {body}");
        }
    }
}