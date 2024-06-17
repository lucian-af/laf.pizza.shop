using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PizzaShop.API.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeColumnNameStatusTableOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Order_Status",
                table: "orders");

            migrationBuilder.RenameColumn(
                name: "status_new",
                table: "orders",
                newName: "status");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Order_Status",
                table: "orders",
                sql: "status in (0,1,2,3,4)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Order_Status",
                table: "orders");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "orders",
                newName: "status_new");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Order_Status",
                table: "orders",
                sql: "status_new in (0,1,2,3,4)");
        }
    }
}
