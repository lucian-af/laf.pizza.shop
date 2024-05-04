using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PizzaShop.API.Infrastructure.Migrations
{
	/// <inheritdoc />
	public partial class CreateTableUsers : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterDatabase()
				.Annotation("Npgsql:Enum:roleuser", "manager,customer");

			migrationBuilder.CreateTable(
				name: "users",
				columns: table => new
				{
					id = table.Column<Guid>(type: "uuid", nullable: false),
					name = table.Column<string>(type: "text", nullable: false),
					email = table.Column<string>(type: "text", nullable: false),
					phone = table.Column<string>(type: "text", nullable: true),
					role = table.Column<byte>(type: "roleuser", nullable: false),
					createdAt = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
					updatedAt = table.Column<DateTime>(type: "timestamp", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_users", x => x.id);
				});

			migrationBuilder.CreateIndex(
				name: "IX_users_email",
				table: "users",
				column: "email",
				unique: true);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "users");
		}
	}
}
