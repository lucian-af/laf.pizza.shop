using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PizzaShop.API.Infrastructure.Migrations
{
	/// <inheritdoc />
	public partial class CreateTableAuthLinks : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "authLinks",
				columns: table => new
				{
					id = table.Column<Guid>(type: "uuid", nullable: false),
					code = table.Column<string>(type: "text", nullable: false),
					userId = table.Column<Guid>(type: "uuid", nullable: false),
					createdAt = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
					updatedAt = table.Column<DateTime>(type: "timestamp", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_authLinks", x => x.id);
					table.ForeignKey(
						name: "FK_authLinks_users_userId",
						column: x => x.userId,
						principalTable: "users",
						principalColumn: "id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_authLinks_code",
				table: "authLinks",
				column: "code",
				unique: true);

			migrationBuilder.CreateIndex(
				name: "IX_authLinks_userId",
				table: "authLinks",
				column: "userId",
				unique: true);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "authLinks");
		}
	}
}