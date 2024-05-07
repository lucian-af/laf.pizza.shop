using System.Net.Mail;

namespace PizzaShop.API.Smtp.Builders
{
	public interface IMailBuilder
	{
		public SmtpClient GetClient();
	}
}