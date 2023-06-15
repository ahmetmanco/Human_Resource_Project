using HumanResource.Application.Models.VMs.EmailVM;
using System.Net;
using System.Net.Mail;

namespace HumanResource.Application.Services.EmailSenderService
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfiguration _configuration;

        public EmailService(EmailConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendEmail(Message message)
        {
            var emailMessage = CreateEmailMessage(message);
            Send(emailMessage);
        }

        public void Send(MailMessage mailMessage)
        {
            using (var client = new SmtpClient(_configuration.SmtpServer, _configuration.Port))
            {
                try
                {
                    client.UseDefaultCredentials = false;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.Credentials = new NetworkCredential( _configuration.From, _configuration.Password);
                    client.EnableSsl = true;


                    client.Send(mailMessage);
                }
                catch (Exception)
                {

                    throw;
                }

            }
        }

        public MailMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MailMessage();
            emailMessage.From = new MailAddress(_configuration.From);
            emailMessage.To.Add(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = message.Content;
            emailMessage.IsBodyHtml= true;

            return emailMessage;
        }
    }
}
