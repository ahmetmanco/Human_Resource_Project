using HumanResource.Application.Services.EmailSenderService;
using HumanResource.Application.Models.VMs.EmailVM;
using HumanResource.Domain.Entities;
using HumanResource.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mail;
using HumanResource.Application.Models.DTOs.AccountDTO;
using Microsoft.AspNetCore.Identity;

namespace HumanResource.Application.Extensions
{
	public class TestMailAttribute : ValidationAttribute
	{
		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			AppUser user = new AppUser();
			var Dogrulama = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext));

			if (Dogrulama.Companies.Any(x => x.CompanyName == value))
			{
                MailMessage mailMessage = new MailMessage();
                mailMessage.Subject = "E-Mail Notification";
                mailMessage.From = new MailAddress("humanresourcehs8@gmail.com", "Human Resources");
				mailMessage.To.Add(new MailAddress("user.Email","user.FirstName"));
				mailMessage.Body = $"Hello {user.FirstName}, Welcome to our human resources platform.Your request has been received. Notification will be made as soon as possible." + mailMessage.From.Address;
				mailMessage.IsBodyHtml = true;
                mailMessage.Priority = MailPriority.High;
                NetworkCredential AccountInfo = new NetworkCredential("user.Email","user.PasswordHash");
                SmtpClient smtp = new SmtpClient("user.Email",587);
                smtp.Send(mailMessage);

			}
			return ValidationResult.Success;
		}
		//return new ValidationResult("Email exists");
	}
}

