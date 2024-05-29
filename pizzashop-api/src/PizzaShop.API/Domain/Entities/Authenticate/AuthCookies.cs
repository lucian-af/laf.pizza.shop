namespace PizzaShop.API.Domain.Entities.Authenticate
{
	public static class AuthCookies
	{
		private static int _totalDaysAuthLinkExpiration = 7;
		private static string authCookieName = "pizza-shop-auth";

		public static string AuthCookieName
		{
			get => authCookieName;
			set => authCookieName = value;
		}

		public static int TotalDaysAuthLinkExpiration
		{
			get => _totalDaysAuthLinkExpiration;
			private set => _totalDaysAuthLinkExpiration = value;
		}
	}
}