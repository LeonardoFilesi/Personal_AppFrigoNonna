using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppFrigoNonna.Migrations
{
    public partial class ExpirationDateNotificationUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ExpDate",
                table: "FridgeProds",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "NextToExp",
                table: "FridgeProds",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpDate",
                table: "FridgeProds");

            migrationBuilder.DropColumn(
                name: "NextToExp",
                table: "FridgeProds");
        }
    }
}
