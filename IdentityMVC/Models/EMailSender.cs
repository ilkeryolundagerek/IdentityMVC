using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace IdentityMVC.Models
{
    public class EMailSender : IEmailSender
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public string Host { get; set; }
        public bool SSL { get; set; }
        public string From { get; set; }

        public EMailSender()
        {

        }

        public EMailSender(string from, string username, string password, int port, string host, bool sSL)
        {
            Username=username;
            Password=password;
            Port=port;
            Host=host;
            SSL=sSL;
            From= from;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            SmtpClient client = new SmtpClient
            {
                Port= Port,
                Host=Host,
                EnableSsl = SSL,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials=false,
                Credentials = new NetworkCredential(Username, Password)
            };

            await client.SendMailAsync(From, email, subject, htmlMessage);
        }
    }
}
