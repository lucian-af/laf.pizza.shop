using System.Net.Mail;
using Microsoft.Extensions.Options;
using PizzaShop.API.Settings;
using PizzaShop.API.Smtp.Builders;
using PizzaShop.API.Smtp.CommonMessages;

namespace PizzaShop.API.Smtp.Adapters
{
	public class MailAdapter(IMailBuilder mailBuilder, IOptions<MailSettings> mailSettings) : IMailAdapter
	{
		private readonly SmtpClient _smtpClient = mailBuilder.GetClient();
		private readonly MailSettings _mailSettings = mailSettings.Value;

		public void SendMail(EmailMessage mailData)
			=> _smtpClient.Send(_mailSettings.SenderEmail, mailData.To, mailData.Title, mailData.Body);
	}
}