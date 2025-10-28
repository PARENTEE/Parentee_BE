using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parentee_BE.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDiaperChangeEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "rash_observed",
                table: "diaper_change");

            migrationBuilder.AddColumn<string>(
                name: "color",
                table: "diaper_change",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "diaper_quantity",
                table: "diaper_change",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "waste",
                table: "diaper_change",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "color",
                table: "diaper_change");

            migrationBuilder.DropColumn(
                name: "diaper_quantity",
                table: "diaper_change");

            migrationBuilder.DropColumn(
                name: "waste",
                table: "diaper_change");

            migrationBuilder.AddColumn<bool>(
                name: "rash_observed",
                table: "diaper_change",
                type: "boolean",
                nullable: true);
        }
    }
}
