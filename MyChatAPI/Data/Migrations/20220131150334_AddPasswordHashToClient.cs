using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyChatAPI.Data.Migrations
{
    public partial class AddPasswordHashToClient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Clients");

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "Clients",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "Clients",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "Clients");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
