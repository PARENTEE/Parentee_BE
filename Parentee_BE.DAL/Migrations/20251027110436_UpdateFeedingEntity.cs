using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parentee_BE.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFeedingEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "amount_ml",
                table: "feeding");

            migrationBuilder.DropColumn(
                name: "duration_min",
                table: "feeding");

            migrationBuilder.DropColumn(
                name: "side",
                table: "feeding");

            migrationBuilder.AddColumn<TimeOnly>(
                name: "left_duration",
                table: "feeding",
                type: "time without time zone",
                nullable: true);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "right_duration",
                table: "feeding",
                type: "time without time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "left_duration",
                table: "feeding");

            migrationBuilder.DropColumn(
                name: "right_duration",
                table: "feeding");

            migrationBuilder.AddColumn<decimal>(
                name: "amount_ml",
                table: "feeding",
                type: "numeric(6,1)",
                precision: 6,
                scale: 1,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "duration_min",
                table: "feeding",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "side",
                table: "feeding",
                type: "text",
                nullable: true);
        }
    }
}
