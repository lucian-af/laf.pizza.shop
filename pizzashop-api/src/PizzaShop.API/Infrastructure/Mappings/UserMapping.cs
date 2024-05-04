using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PizzaShop.API.Domain;
using PizzaShop.API.Domain.Enums;
using PizzaShop.API.Infrastructure.Extensions;

namespace PizzaShop.API.Infrastructure.Mappings
{
	public class UserMapping : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.AddMappingForAuditableProperties();

			builder
				.ToTable("users",
				  u => u.HasCheckConstraint("CK_Users_Role", "role in ('manager', 'customer')"))
				.HasKey(u => u.Id);

			builder
				.Property(u => u.Id)
				.HasColumnName("id");

			builder
				.Property(u => u.Name)
				.HasColumnType("text")
				.IsRequired()
				.HasColumnName("name");

			builder
				.Property(u => u.Email)
				.HasColumnType("text")
				.IsRequired()
				.HasColumnName("email");

			builder
				.HasIndex(u => u.Email)
				.IsUnique();

			builder
				.Property(u => u.Phone)
				.HasColumnType("text")
				.IsRequired(false)
				.HasColumnName("phone");

			builder
				.Property(u => u.Role)
				.HasColumnType("text")
				.IsRequired()
				.HasColumnName("role")
				.HasConversion(EnumConverter.EnumToStringConverter<RoleUser>());
		}
	}

	public static class EnumConverter
	{
		public static ValueConverter EnumToStringConverter<T>()
			=> new ValueConverter<T, string>(
				enumTo => enumTo.ToString().ToLower(),
				enumFrom => (T)Enum.Parse(typeof(T), enumFrom));
	}
}
