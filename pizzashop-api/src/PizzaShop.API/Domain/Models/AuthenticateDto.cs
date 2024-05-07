using System.ComponentModel.DataAnnotations;

namespace PizzaShop.API.Domain.Models
{
	public class AuthenticateDto
	{
		[EmailAddress(ErrorMessage = "E-mail invalid")]
		[Required(ErrorMessage = "E-mail is required!")]
		public string Email { get; set; }
	}
}