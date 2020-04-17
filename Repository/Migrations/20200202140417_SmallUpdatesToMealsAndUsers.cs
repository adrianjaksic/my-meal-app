using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class SmallUpdatesToMealsAndUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Colories",
                table: "Meals");

            migrationBuilder.AddColumn<int>(
                name: "Calories",
                table: "Meals",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Time",
                table: "Meals",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Calories",
                table: "Meals");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "Meals");

            migrationBuilder.AddColumn<int>(
                name: "Colories",
                table: "Meals",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
