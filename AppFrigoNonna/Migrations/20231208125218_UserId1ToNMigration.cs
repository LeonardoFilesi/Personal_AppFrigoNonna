using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppFrigoNonna.Migrations
{
    public partial class UserId1ToNMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "FridgeProds",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "FridgeProds");
        }
    }
}
