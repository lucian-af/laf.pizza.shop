using PizzaShop.API.Smtp.CommonMessages;

namespace PizzaShop.API.Smtp.Adapters
{
	public interface IMailAdapter
	{
		void SendMail(EmailMessage mailData);
	}
}