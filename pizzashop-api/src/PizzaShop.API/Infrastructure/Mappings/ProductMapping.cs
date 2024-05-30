using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PizzaShop.API.Domain.Entities.Shops;
using PizzaShop.API.Infrastructure.Extensions;

namespace PizzaShop.API.Infrastructure.Mappings
{
	public class ProductMapping : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder.AddMappingForAuditableProperties();

			builder.ToTable("products")
				.HasKey(u => u.Id);

			builder
				.Property(u => u.Id)
				.HasColumnName("id");

			builder
				.Property(u => u.Name)
				.HasColumnName("name")
				.HasColumnType("text")
				.IsRequired();

			builder
				.Property(u => u.Description)
				.HasColumnName("description")
				.HasColumnType("text")
				.IsRequired(false);

			builder
				.Property(u => u.Price)
				.HasColumnName("price")
				.HasColumnType("money")
				.IsRequired();

			builder
				.Property(u => u.RestaurantId)
				.HasColumnName("restaurantId");

			builder.HasOne(r => r.Restaurant)
				.WithMany(u => u.Products)
				.HasForeignKey(r => r.RestaurantId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Cascade)
				.HasConstraintName("product_restaurant");
		}
	}
}