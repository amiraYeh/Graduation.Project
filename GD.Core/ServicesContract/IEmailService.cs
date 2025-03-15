
using GP.Focusi.Core.Entites.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace GP.Focusi.Core.ServicesContract
{
	public interface IEmailService//: IEmailSender
	{
		 Task SendEMailAsync(string email, string subject, string htmlMessage);
		
	}
}
