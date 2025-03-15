using Castle.Core.Smtp;
using GP.Focusi.Core.Entites.Identity;
using GP.Focusi.Core.ServicesContract;
using Microsoft.Extensions.Configuration;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace GP.Focusi.Services.Users
{
	 
	public class MailService:IEmailService
	{

		public async Task SendEMailAsync(string email, string subject, string htmlMessage)
		{
			var smtpClient = new SmtpClient("smtp.gmail.com", 587);
			smtpClient.EnableSsl = true;

			smtpClient.Credentials = new NetworkCredential("focusisystem5@gmail.com", "tuqmfqcasvoovoah");
			var mailMessage = new MailMessage
			{
				From = new MailAddress("focusisystem5@gmail.com"),
				Subject = subject,
				Body = htmlMessage,
				IsBodyHtml = true,
			};
			mailMessage.To.Add(email);

			await smtpClient.SendMailAsync(mailMessage);
		}

		//public Task SendEMailAsync(string email, string subject, string htmlMessage)
		//{
		//	throw new NotImplementedException();
		//}
		//XRWRUKSN3MCEM3A9WG3TNBHY
		//public async Task SendMailAsync(SendingEmail email)
		//{
		//	try
		//	{
		//		var clientSmtp = new SmtpClient("smtp.gmail.com", 587);
		//		clientSmtp.EnableSsl = true;

		//		clientSmtp.Credentials = new NetworkCredential("focusisystem5@gmail.com", "tuqmfqcasvoovoah");

		//		clientSmtp.Send("focusisystem5@gmail.com", email.To, email.Subject, email.Body);
		//	}
		//	catch (Exception ex)
		//	{
		//              Console.WriteLine($"Error : {ex}");
		//	}


		//	MailSettings mailSettings = new MailSettings();


		//}
	}
}
