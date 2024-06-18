using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;
using PizzaShop.API.Settings;

namespace PizzaShop.API.Infrastructure.Context
{
	public class PizzaShopContextFactory : IDesignTimeDbContextFactory<PizzaShopContext>
	{
		public PizzaShopContext CreateDbContext(string[] args)
		{
			var configuration = new ConfigurationBuilder()
					.SetBasePath(Directory.GetCurrentDirectory())
					.AddJsonFile("appsettings.Development.json")
					.Build();

			var optionsBuilder = new DbContextOptionsBuilder<PizzaShopContext>();
			optionsBuilder.UseNpgsql(configuration.GetConnectionString("PizzaShop"));

			return new PizzaShopContext(optionsBuilder.Options, Options.Create(new PizzaShopConfigs { Mode = ModeApplication.PRESENTATION }));
		}
	}
}