using System.Globalization;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace PizzaShop.API.Domain.Helpers
{
	public static class EnumConverter
	{
		public static ValueConverter EnumToStringConverter<T>()
			=> new ValueConverter<T, string>(
				enumTo => enumTo.ToString().ToLower(),
				enumFrom => (T)Enum.Parse(typeof(T), CultureInfo.CurrentCulture.TextInfo.ToTitleCase(enumFrom)));
	}
}