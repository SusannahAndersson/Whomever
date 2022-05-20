//Only used to log ui to console
//Interface: in case to actually implement extended mail service usage (with auto ext)
namespace Whomever.Areas.ContactUsService
{
    public interface IContactUsService
    {
        void SendMessage(string to, string subject, string body);
    }
}