using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PizzaShop.API.Infrastructure.Migrations
{
	/// <inheritdoc />
	public partial class CreateTableRestaurants : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "restaurants",
				columns: table => new
				{
					id = table.Column<Guid>(type: "uuid", nullable: false),
					name = table.Column<string>(type: "text", nullable: false),
					description = table.Column<string>(type: "text", nullable: true),
					createdAt = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
					updatedAt = table.Column<DateTime>(type: "timestamp", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_restaurants", x => x.id);
				});
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "restaurants");
		}
	}
}
