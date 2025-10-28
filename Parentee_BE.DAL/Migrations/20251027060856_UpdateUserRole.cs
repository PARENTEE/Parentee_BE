using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parentee_BE.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_family_created_by",
                table: "family");

            migrationBuilder.CreateIndex(
                name: "IX_family_created_by",
                table: "family",
                column: "created_by",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_family_created_by",
                table: "family");

            migrationBuilder.CreateIndex(
                name: "IX_family_created_by",
                table: "family",
                column: "created_by");
        }
    }
}
