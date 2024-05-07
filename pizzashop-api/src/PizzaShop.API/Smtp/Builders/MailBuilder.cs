using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using PizzaShop.API.Settings;

namespace PizzaShop.API.Smtp.Builders
{
	public class MailBuilder(IOptions<MailSettings> mailSettings) : IMailBuilder
	{
		private readonly MailSettings _mailSettings = mailSettings.Value;

		public SmtpClient GetClient()
		{
			var client = new SmtpClient(_mailSettings.Server, _mailSettings.Port)
			{
				Credentials = new NetworkCredential(_mailSettings.UserName, _mailSettings.Password),
				EnableSsl = true
			};

			return client;
		}
	}
}