//Only used to log ui to console
//Interface: in case to actually implement extended mail service usage (with auto ext)
namespace Whomever.Areas.ContactUsService
{
    public class NullContactUsService : IContactUsService
    {
        private readonly ILogger<NullContactUsService> _logger;

        public NullContactUsService(ILogger<NullContactUsService> logger)
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