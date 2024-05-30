using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PizzaShop.API.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateTablesOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    customerId = table.Column<Guid>(type: "uuid", nullable: true),
                    restaurantId = table.Column<Guid>(type: "uuid", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false, defaultValue: "pending"),
                    valorTotal = table.Column<decimal>(type: "money", nullable: false),
                    createdAt = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updatedAt = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.id);
                    table.CheckConstraint("CK_Order_Status", "status in ('pending', 'processing', 'delivering', 'delivered', 'canceled')");
                    table.ForeignKey(
                        name: "order_customer",
                        column: x => x.customerId,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "order_resturant",
                        column: x => x.restaurantId,
                        principalTable: "restaurants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    price = table.Column<decimal>(type: "money", nullable: false),
                    restaurantId = table.Column<Guid>(type: "uuid", nullable: false),
                    createdAt = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updatedAt = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.id);
                    table.ForeignKey(
                        name: "product_restaurant",
                        column: x => x.restaurantId,
                        principalTable: "restaurants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "orderItems",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    orderId = table.Column<Guid>(type: "uuid", nullable: false),
                    productId = table.Column<Guid>(type: "uuid", nullable: true),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    price = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orderItems", x => x.id);
                    table.ForeignKey(
                        name: "orderItems_order",
                        column: x => x.orderId,
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "orderItems_product",
                        column: x => x.productId,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_orderItems_orderId",
                table: "orderItems",
                column: "orderId");

            migrationBuilder.CreateIndex(
                name: "IX_orderItems_productId",
                table: "orderItems",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_orders_customerId",
                table: "orders",
                column: "customerId");

            migrationBuilder.CreateIndex(
                name: "IX_orders_restaurantId",
                table: "orders",
                column: "restaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_products_restaurantId",
                table: "products",
                column: "restaurantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "orderItems");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "products");
        }
    }
}
