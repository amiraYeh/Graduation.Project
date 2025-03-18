using GP.Focusi.Core.ServicesContract;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;


namespace GP.Focusi.Services
{
	
	public class EmailSenderService:IEmailSenderService
	{
		private readonly IConfiguration _configuration;

		public EmailSenderService(IConfiguration configuration)
        {
			_configuration = configuration;
		}

        public void SendAnEmail(string receiverEmail, string subject, string message)
		{
			string? senderEmail =_configuration["BrevoEmailsApi:SenderEmail"];
			
			string? senderName = _configuration["BrevoEmailsApi:SenderName"];

			var apiInstance = new TransactionalEmailsApi();

			SendSmtpEmailSender sender = new SendSmtpEmailSender(senderName, senderEmail);

			SendSmtpEmailTo receiver1 = new SendSmtpEmailTo(receiverEmail);
			List<SendSmtpEmailTo> To = new List<SendSmtpEmailTo>();
			To.Add(receiver1);

			string HtmlContent = null;
			string TextContent = message;

			try
			{
				var sendSmtpEmail = new SendSmtpEmail(sender, To, null, null, HtmlContent, TextContent, subject);
				CreateSmtpEmail result = apiInstance.SendTransacEmail(sendSmtpEmail);
				Console.WriteLine(result.ToJson());
				Console.ReadLine();
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				Console.ReadLine();
			}

		}
	}
}
