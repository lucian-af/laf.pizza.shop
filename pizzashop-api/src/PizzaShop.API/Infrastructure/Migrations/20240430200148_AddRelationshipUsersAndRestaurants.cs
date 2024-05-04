using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PizzaShop.API.Infrastructure.Migrations
{
	/// <inheritdoc />
	public partial class AddRelationshipUsersAndRestaurants : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<Guid>(
				name: "managerId",
				table: "restaurants",
				type: "uuid",
				nullable: true);

			migrationBuilder.CreateIndex(
				name: "IX_restaurants_managerId",
				table: "restaurants",
				column: "managerId");

			migrationBuilder.AddForeignKey(
				name: "restaurant_manager",
				table: "restaurants",
				column: "managerId",
				principalTable: "users",
				principalColumn: "id",
				onDelete: ReferentialAction.SetNull);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "restaurant_manager",
				table: "restaurants");

			migrationBuilder.DropIndex(
				name: "IX_restaurants_managerId",
				table: "restaurants");

			migrationBuilder.DropColumn(
				name: "managerId",
				table: "restaurants");
		}
	}
}
