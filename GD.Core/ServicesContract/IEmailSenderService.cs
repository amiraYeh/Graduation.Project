using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Focusi.Core.ServicesContract
{
	public interface IEmailSenderService
	{
		public string SendAnEmail(string receiverEmail, string subject, string message);
	}
}
