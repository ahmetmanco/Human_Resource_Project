using HumanResource.Application.Models.VMs.EmailVM;
using MimeKit;
using System.Net.Mail;

namespace HumanResource.Application.Services.EmailSenderService
{
    public interface IEmailService
    {
        void SendEmail(Message message);
        MailMessage CreateEmailMessage(Message message);
        void Send(MailMessage mailMessage);
    }
}
