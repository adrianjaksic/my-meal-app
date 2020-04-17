using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class PasswordChangeToBytes2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password1",
                table: "Users");

            migrationBuilder.AddColumn<byte[]>(
                name: "Password",
                table: "Users",
                maxLength: 20,
                nullable: false,
                defaultValue: new byte[] {  });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Users");

            migrationBuilder.AddColumn<byte[]>(
                name: "Password1",
                table: "Users",
                type: "varbinary(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: new byte[] {  });
        }
    }
}
