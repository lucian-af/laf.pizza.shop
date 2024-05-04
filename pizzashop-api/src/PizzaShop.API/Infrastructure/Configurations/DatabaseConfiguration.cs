using Microsoft.EntityFrameworkCore;
using PizzaShop.API.Infrastructure.Context;

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

			return services;
		}
	}
}
