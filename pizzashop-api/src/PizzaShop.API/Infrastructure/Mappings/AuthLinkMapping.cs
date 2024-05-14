using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PizzaShop.API.Domain.Entities;
using PizzaShop.API.Infrastructure.Extensions;

namespace PizzaShop.API.Infrastructure.Mappings
{
	public class AuthLinkMapping : IEntityTypeConfiguration<AuthLink>
	{
		public void Configure(EntityTypeBuilder<AuthLink> builder)
		{
			builder.AddMappingForAuditableProperties();

			builder
				.ToTable("authLinks")
				.HasKey(u => u.Id);

			builder
				.Ignore(al => al.AuthLinkExpiration);

			builder
				.Property(u => u.Id)
				.HasColumnName("id");

			builder
				.Property(u => u.Code)
				.HasColumnName("code")
				.HasColumnType("text")
				.IsRequired();

			builder
				.HasIndex(u => u.Code)
				.IsUnique();

			builder
				.Property(u => u.UserId)
				.HasColumnName("userId")
				.IsRequired();

			builder
				.HasOne(al => al.User)
				.WithMany()
				.HasForeignKey(al => al.UserId)
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired()
				.HasConstraintName("authLink_user");
		}
	}
}