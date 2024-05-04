using PizzaShop.API.Infrastructure.Configurations;
using PizzaShop.API.Settings;
using PizzaShop.API.UseCases;

namespace PizzaShop.API.Configurations
{
	public static class ServicesConfiguration
	{
		public static IServiceCollection AddWebApiConfiguration(this IServiceCollection services, IConfiguration configuration)
		{
			if (configuration is null)
				throw new ArgumentNullException(nameof(configuration), "IConfiguration is null.");

			services.AddEndpointsApiExplorer();
			services.AddSwaggerGen();
			services.AddDatabaseConfiguration(configuration.GetConnectionString("PizzaShop"));
			services.Configure<PizzaShopConfigs>(configuration.GetSection(nameof(PizzaShopConfigs)));

			services.AddServicesConfiguration();

			return services;
		}

		public static IServiceCollection AddServicesConfiguration(this IServiceCollection services)
		{
			services.AddScoped<AddRestaurant>();

			return services;
		}
	}
}