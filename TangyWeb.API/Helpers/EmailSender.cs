using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;
using MimeKit.Text;

namespace TangyWeb.API.Helpers
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {
                var mail = new MimeMessage();
                mail.From.Add(MailboxAddress.Parse("foreach@gmail.com"));
                mail.To.Add(MailboxAddress.Parse(email));
                mail.Subject = subject;
                mail.Body = new TextPart(TextFormat.Html) { Text = htmlMessage };

                var client = new SmtpClient();
                client.CheckCertificateRevocation = false;
                client.Connect("smtp.gmail.com", 587, false);
                //need to use app-password as now less secured app is not supported anymore
                client.Authenticate("t0088194@gmail.com", "Test13!#~");
                client.Send(mail);
                client.Disconnect(true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
