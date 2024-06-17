using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PizzaShop.API.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeColumnTypeStatusTableOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Order_Status",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "status",
                table: "orders");

            migrationBuilder.AddColumn<short>(
                name: "status_new",
                table: "orders",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddCheckConstraint(
                name: "CK_Order_Status",
                table: "orders",
                sql: "status_new in (0,1,2,3,4)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Order_Status",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "status_new",
                table: "orders");

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "orders",
                type: "text",
                nullable: false,
                defaultValue: "pending");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Order_Status",
                table: "orders",
                sql: "status in ('pending', 'processing', 'delivering', 'delivered', 'canceled')");
        }
    }
}
