using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PizzaShop.API.Domain;
using PizzaShop.API.Domain.Enums;
using PizzaShop.API.Infrastructure.Context;
using PizzaShop.API.Settings;

namespace PizzaShop.API.Infrastructure.Configurations
{
	public static class DatabaseMigrations
	{
		public static void RunMigrations(this IApplicationBuilder app, IOptions<PizzaShopConfigs> configs)
		{
			using var serviceScope = app.ApplicationServices
				.GetRequiredService<IServiceScopeFactory>()
				.CreateScope();
			var pizzaShopContext = serviceScope.ServiceProvider.GetRequiredService<PizzaShopContext>();
			pizzaShopContext.Database.Migrate();

#if DEBUG
			if (configs.Value.Mode == ModeApplication.PRESENTATION)
				GenerateSeed(configs, pizzaShopContext);
#endif
		}

		private static void GenerateSeed(IOptions<PizzaShopConfigs> configs, PizzaShopContext pizzaShopContext)
		{
			pizzaShopContext.Restaurants.ExecuteDelete();
			pizzaShopContext.Users.ExecuteDelete();
			Console.ForegroundColor = ConsoleColor.DarkYellow;
			Console.WriteLine($"[Mode:{configs.Value.Mode}] - Database reset...");

			var (users, restaurants) = DataFake.Generate();
			Console.ForegroundColor = ConsoleColor.DarkYellow;
			Console.WriteLine($"[Mode:{configs.Value.Mode}] - Generating data...");

			pizzaShopContext.Users.AddRange(users);
			pizzaShopContext.Restaurants.AddRange(restaurants);
			pizzaShopContext.Commit().Wait();

			Console.ForegroundColor = ConsoleColor.DarkGreen;
			Console.WriteLine($"[Mode:{configs.Value.Mode}] - Successfully executed seed!");
		}
	}

	public static class DataFake
	{
		public static (User, Restaurant) Generate()
		{
			var faker = new Faker("pt_BR");
			var user = new User(faker.Person.FullName, "admin@admin.com", faker.Person.Phone, RoleUser.Manager);
			var restaurant = new Restaurant(faker.Company.CompanyName(), faker.Company.CatchPhrase(), user.Role == RoleUser.Manager ? user : null);
			return (user, restaurant);
		}
	}
}