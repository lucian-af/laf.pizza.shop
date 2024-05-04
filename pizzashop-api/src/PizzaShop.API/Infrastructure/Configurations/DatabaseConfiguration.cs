using Microsoft.EntityFrameworkCore;
using PizzaShop.API.Domain.Interfaces;
using PizzaShop.API.Infrastructure.Context;
using PizzaShop.API.Infrastructure.Repositories;

namespace PizzaShop.API.Infrastructure.Configurations
{
	public static class DatabaseConfiguration
	{
		public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services, string connectionStrings)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(nameof(connectionStrings));

			services.AddDbContext<PizzaShopContext>(opt
				=> opt.UseNpgsql(connectionStrings, npg =>
				{
					npg.EnableRetryOnFailure(3);
				}).EnableDetailedErrors(true));

			services.AddRepositoryDependencies();

			return services;
		}

		public static IServiceCollection AddRepositoryDependencies(this IServiceCollection services)
		{
			services.AddScoped<IRestaurantRepository, RestaurantRepository>();

			return services;
		}
	}
}