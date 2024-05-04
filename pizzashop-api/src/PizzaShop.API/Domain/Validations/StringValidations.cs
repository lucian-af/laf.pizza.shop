using System.Text.RegularExpressions;

namespace PizzaShop.API.Domain
{
	public static partial class StringValidations
	{
		[GeneratedRegex("\\D")]
		public static partial Regex ClearSpecialCharacters();
	}
}
