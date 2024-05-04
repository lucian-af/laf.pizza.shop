namespace PizzaShop.API.Settings
{
	public class PizzaShopConfigs
	{
		public ModeApplication Mode { get; set; } = ModeApplication.PRODUCTION;
	}

	public enum ModeApplication
	{
		PRODUCTION,
		PRESENTATION
	}
}
