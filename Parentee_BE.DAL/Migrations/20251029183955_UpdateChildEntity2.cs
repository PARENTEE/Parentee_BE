using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parentee_BE.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateChildEntity2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "sex",
                table: "child");

            migrationBuilder.AddColumn<string>(
                name: "gender",
                table: "child",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "gender",
                table: "child");

            migrationBuilder.AddColumn<string>(
                name: "sex",
                table: "child",
                type: "text",
                nullable: true);
        }
    }
}
