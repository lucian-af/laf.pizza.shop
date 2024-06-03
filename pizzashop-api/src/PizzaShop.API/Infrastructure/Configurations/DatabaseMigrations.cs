using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PizzaShop.API.Domain.Entities.Authenticate;
using PizzaShop.API.Domain.Entities.Orders;
using PizzaShop.API.Domain.Entities.Shops;
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
				GenerateSeed(configs.Value, pizzaShopContext);
#endif
		}

		private static void GenerateSeed(PizzaShopConfigs configs, PizzaShopContext pizzaShopContext)
		{
			pizzaShopContext.Restaurants.ExecuteDelete();
			pizzaShopContext.Users.ExecuteDelete();
			Console.ForegroundColor = ConsoleColor.DarkYellow;
			Console.WriteLine($"[Mode:{configs.Mode}] - Database reset...");

			var users = DataFake.GenerateCustomers();
			var restaurant = DataFake.GenerateRestaurant(configs.ManagerEmail);
			var products = DataFake.GenerateProducts(restaurant.Id);
			users.Add(restaurant.Manager);
			var orders = DataFake.GenerateOrders(restaurant.Id, products, users);
			Console.ForegroundColor = ConsoleColor.DarkYellow;
			Console.WriteLine($"[Mode:{configs.Mode}] - Generating data...");

			pizzaShopContext.Users.AddRange(users.FindAll(u => u.Id != restaurant.ManagerId));
			pizzaShopContext.Restaurants.Add(restaurant);
			pizzaShopContext.Products.AddRange(products);
			pizzaShopContext.Orders.AddRange(orders);
			pizzaShopContext.Commit().Wait();

			Console.ForegroundColor = ConsoleColor.DarkGreen;
			Console.WriteLine($"[Mode:{configs.Mode}] - Successfully executed seed!");
		}
	}

	public static class DataFake
	{
		private static Faker _faker = new("pt_BR");

		public static List<User> GenerateCustomers()
		{
			List<User> users = [];
			for (var i = 1; i <= 3; i++)
			{
				_faker = new("pt_BR");
				users.Add(new(_faker.Person.FullName, _faker.Person.Email, _faker.Person.Phone, RoleUser.Manager));
			}

			return users;
		}

		public static Restaurant GenerateRestaurant(string emailFake = null)
		{
			var restaurant = new Restaurant(
				_faker.Company.CompanyName(),
				_faker.Company.CatchPhrase(),
				_faker.Person.FullName,
				emailFake ?? _faker.Person.Email,
				_faker.Person.Phone);

			return restaurant;
		}

		public static List<Product> GenerateProducts(Guid restaurantId)
		{
			var products = new List<Product>();
			for (var i = 1; i <= 10; i++)
			{
				_faker = new("pt_BR");
				products.Add(new(
					_faker.Commerce.ProductName(),
					_faker.Commerce.ProductDescription(),
					decimal.Parse(_faker.Commerce.Price(min: 190, max: 490)),
					restaurantId));
			}
			return products;
		}

		public static List<Order> GenerateOrders(Guid restaurantId, List<Product> products, List<User> customers)
		{
			var orders = new List<Order>();
			for (var i = 1; i <= 200; i++)
			{
				_faker = new("pt_BR");
				var customer = _faker.PickRandom(customers);
				Order order = new(customer.Id, restaurantId);
				var productsItem = _faker.PickRandom(products, _faker.Random.Int(1, 3)).ToList();
				productsItem.ForEach(productItem =>
				{
					var quantity = _faker.Random.Int(1, 3);
					order.AddItem(productItem.Id, quantity, productItem.Price);
				});
				var status = _faker.PickRandom<OrderStatus>();
				order.ChangeStatus(status);

				orders.Add(order);
			}
			return orders;
		}
	}
}