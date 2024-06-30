using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PizzaShop.API.Domain.Entities;
using PizzaShop.API.Domain.Entities.Authenticate;
using PizzaShop.API.Domain.Entities.Orders;
using PizzaShop.API.Domain.Entities.Shops;
using PizzaShop.API.Infrastructure.Data;
using PizzaShop.API.Settings;

namespace PizzaShop.API.Infrastructure.Context
{
	public class PizzaShopContext(DbContextOptions options, IOptions<PizzaShopConfigs> configs) : DbContext(options), IUnitOfWork
	{
		private readonly PizzaShopConfigs configs = configs.Value;

		public DbSet<User> Users { get; set; }
		public DbSet<Restaurant> Restaurants { get; set; }
		public DbSet<AuthLink> AuthLinks { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderItem> OrderItems { get; set; }
		public DbSet<Product> Products { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(PizzaShopContext).Assembly);
			base.OnModelCreating(modelBuilder);
		}

		public async Task<bool> Commit()
		{
			foreach (var entry in ChangeTracker.Entries<Auditable>().ToList())
			{
				switch (entry.State)
				{
					case EntityState.Added:
						if (configs.Mode == ModeApplication.PRESENTATION && entry.Entity.ToString().Split('.').LastOrDefault().Equals(nameof(Order)))
						{
							var faker = new Faker("pt_BR");
							entry.Entity.CreatedAt = faker.Date.Between(DateTime.Now.AddMonths(-1), DateTime.Now);
						}
						else
							entry.Entity.CreatedAt = DateTime.Now;
						break;

					case EntityState.Modified:
						entry.Entity.UpdatedAt = DateTime.Now;
						break;
				}
			}

			return await base.SaveChangesAsync() > 0;
		}
	}
}