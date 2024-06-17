﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PizzaShop.API.Infrastructure.Context;

#nullable disable

namespace PizzaShop.API.Infrastructure.Migrations
{
    [DbContext(typeof(PizzaShopContext))]
    [Migration("20240616150005_ChangeColumnTypeStatusTableOrder")]
    partial class ChangeColumnTypeStatusTableOrder
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PizzaShop.API.Domain.Entities.Authenticate.AuthLink", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("code");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp")
                        .HasColumnName("createdAt")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("updatedAt");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("userId");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("authLinks", (string)null);
                });

            modelBuilder.Entity("PizzaShop.API.Domain.Entities.Authenticate.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp")
                        .HasColumnName("createdAt")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Phone")
                        .HasColumnType("text")
                        .HasColumnName("phone");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("role");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("updatedAt");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("users", null, t =>
                        {
                            t.HasCheckConstraint("CK_Users_Role", "role in ('manager', 'customer')");
                        });
                });

            modelBuilder.Entity("PizzaShop.API.Domain.Entities.Orders.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp")
                        .HasColumnName("createdAt")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<Guid?>("CustomerId")
                        .HasColumnType("uuid")
                        .HasColumnName("customerId");

                    b.Property<Guid>("RestaurantId")
                        .HasColumnType("uuid")
                        .HasColumnName("restaurantId");

                    b.Property<short>("Status_New")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint")
                        .HasDefaultValue((short)0)
                        .HasColumnName("status_new");

                    b.Property<decimal>("Total")
                        .HasColumnType("money")
                        .HasColumnName("total");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("updatedAt");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("RestaurantId");

                    b.ToTable("orders", null, t =>
                        {
                            t.HasCheckConstraint("CK_Order_Status", "status_new in (0,1,2,3,4)");
                        });
                });

            modelBuilder.Entity("PizzaShop.API.Domain.Entities.Orders.OrderItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uuid")
                        .HasColumnName("orderId");

                    b.Property<decimal>("Price")
                        .HasColumnType("money")
                        .HasColumnName("price");

                    b.Property<Guid?>("ProductId")
                        .HasColumnType("uuid")
                        .HasColumnName("productId");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer")
                        .HasColumnName("quantity");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("orderItems", (string)null);
                });

            modelBuilder.Entity("PizzaShop.API.Domain.Entities.Shops.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp")
                        .HasColumnName("createdAt")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<decimal>("Price")
                        .HasColumnType("money")
                        .HasColumnName("price");

                    b.Property<Guid>("RestaurantId")
                        .HasColumnType("uuid")
                        .HasColumnName("restaurantId");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("updatedAt");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId");

                    b.ToTable("products", (string)null);
                });

            modelBuilder.Entity("PizzaShop.API.Domain.Entities.Shops.Restaurant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp")
                        .HasColumnName("createdAt")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<Guid?>("ManagerId")
                        .HasColumnType("uuid")
                        .HasColumnName("managerId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("updatedAt");

                    b.HasKey("Id");

                    b.HasIndex("ManagerId");

                    b.ToTable("restaurants", (string)null);
                });

            modelBuilder.Entity("PizzaShop.API.Domain.Entities.Authenticate.AuthLink", b =>
                {
                    b.HasOne("PizzaShop.API.Domain.Entities.Authenticate.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("authLink_user");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PizzaShop.API.Domain.Entities.Orders.Order", b =>
                {
                    b.HasOne("PizzaShop.API.Domain.Entities.Authenticate.User", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("order_customer");

                    b.HasOne("PizzaShop.API.Domain.Entities.Shops.Restaurant", "Restaurant")
                        .WithMany("Orders")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("order_resturant");

                    b.Navigation("Customer");

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("PizzaShop.API.Domain.Entities.Orders.OrderItem", b =>
                {
                    b.HasOne("PizzaShop.API.Domain.Entities.Orders.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("orderItems_order");

                    b.HasOne("PizzaShop.API.Domain.Entities.Shops.Product", "Product")
                        .WithMany("OrderItems")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("orderItems_product");

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("PizzaShop.API.Domain.Entities.Shops.Product", b =>
                {
                    b.HasOne("PizzaShop.API.Domain.Entities.Shops.Restaurant", "Restaurant")
                        .WithMany("Products")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("product_restaurant");

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("PizzaShop.API.Domain.Entities.Shops.Restaurant", b =>
                {
                    b.HasOne("PizzaShop.API.Domain.Entities.Authenticate.User", "Manager")
                        .WithMany("Restaurants")
                        .HasForeignKey("ManagerId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("restaurant_manager");

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("PizzaShop.API.Domain.Entities.Authenticate.User", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("Restaurants");
                });

            modelBuilder.Entity("PizzaShop.API.Domain.Entities.Orders.Order", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("PizzaShop.API.Domain.Entities.Shops.Product", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("PizzaShop.API.Domain.Entities.Shops.Restaurant", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
