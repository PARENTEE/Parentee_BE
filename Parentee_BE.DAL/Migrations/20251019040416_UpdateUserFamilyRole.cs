using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parentee_BE.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserFamilyRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "invitation_status",
                table: "user_family_role",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "invitation_status",
                table: "user_family_role");
        }
    }
}
