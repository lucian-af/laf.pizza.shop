namespace PizzaShop.API.Settings
{
	public class PizzaShopConfigs
	{
		public ModeApplication Mode { get; set; } = ModeApplication.PRODUCTION;
		public string BaseUrl { get; set; }
		public string AppUrl { get; set; }
		public string AppUrlLogin { get; set; }
	}

	public enum ModeApplication
	{
		PRODUCTION,
		PRESENTATION
	}
}