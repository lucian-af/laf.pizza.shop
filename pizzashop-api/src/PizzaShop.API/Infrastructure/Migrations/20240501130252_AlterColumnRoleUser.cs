using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PizzaShop.API.Infrastructure.Migrations
{
	/// <inheritdoc />
	public partial class AlterColumnRoleUser : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<string>(
				name: "role",
				table: "users",
				type: "text",
				nullable: false,
				oldClrType: typeof(byte),
				oldType: "roleuser");

			migrationBuilder.AddCheckConstraint(
				name: "CK_Users_Role",
				table: "users",
				sql: "role in ('manager', 'customer')");

			migrationBuilder.AlterDatabase()
				.OldAnnotation("Npgsql:Enum:roleuser", "manager,customer");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropCheckConstraint(
				name: "CK_Users_Role",
				table: "users");

			migrationBuilder.AlterDatabase()
				.Annotation("Npgsql:Enum:roleuser", "manager,customer");

			migrationBuilder.AlterColumn<byte>(
				name: "role",
				table: "users",
				type: "roleuser",
				nullable: false,
				oldClrType: typeof(string),
				oldType: "text");
		}
	}
}
