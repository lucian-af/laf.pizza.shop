namespace PizzaShop.API.Domain.Extensions
{
	public static class StringExtensions
	{
		public static Guid ToGuid(this string value)
		{
			_ = Guid.TryParse(value, out var result);
			return result;
		}
	}
}