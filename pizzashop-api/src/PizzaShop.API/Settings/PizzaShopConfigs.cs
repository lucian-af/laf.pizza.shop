namespace PizzaShop.API.Settings
{
	public class PizzaShopConfigs
	{
		public ModeApplication Mode { get; set; } = ModeApplication.PRODUCTION;
		public string BaseUrl { get; set; }
		public string AuthRedirectUrl { get; set; }
	}

	public enum ModeApplication
	{
		PRODUCTION,
		PRESENTATION
	}
}