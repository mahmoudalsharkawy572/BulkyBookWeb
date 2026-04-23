using System.Net;
using System.Net.Mail;

namespace Demo.Presentation.Utilities
{
    public static class EmailSettings 
    {
        public static void SendEmail(Email email)
        {
            var client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("Put your official Service mail here", "mhqylacvygjictwc");
            client.Send("Put your official Service mail here", email.To, email.Subject, email.Body);
        }
    }
}
