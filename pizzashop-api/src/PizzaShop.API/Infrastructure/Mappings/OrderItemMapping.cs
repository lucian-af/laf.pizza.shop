using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PizzaShop.API.Domain.Entities.Orders;

namespace PizzaShop.API.Infrastructure.Mappings
{
	public class OrderItemMapping : IEntityTypeConfiguration<OrderItem>
	{
		public void Configure(EntityTypeBuilder<OrderItem> builder)
		{
			builder.ToTable("orderItems")
				.HasKey(u => u.Id);

			builder
				.Property(u => u.Id)
				.HasColumnName("id");

			builder
				.Property(u => u.Quantity)
				.HasColumnName("quantity")
				.HasColumnType("integer")
				.IsRequired();

			builder
				.Property(u => u.Price)
				.HasColumnName("price")
				.HasColumnType("money")
				.IsRequired();

			builder
				.Property(u => u.OrderId)
				.HasColumnName("orderId")
				.IsRequired();

			builder
				.Property(u => u.ProductId)
				.HasColumnName("productId")
				.IsRequired(false);

			builder.HasOne(r => r.Product)
				.WithMany(u => u.OrderItems)
				.HasForeignKey(r => r.ProductId)
				.OnDelete(DeleteBehavior.SetNull)
				.HasConstraintName("orderItems_product");

			builder.HasOne(r => r.Order)
				.WithMany(u => u.OrderItems)
				.HasForeignKey(r => r.OrderId)
				.OnDelete(DeleteBehavior.Cascade)
				.HasConstraintName("orderItems_order");
		}
	}
}