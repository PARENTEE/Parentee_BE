using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parentee_BE.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "order_code",
                table: "purchase",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "order_code",
                table: "purchase");
        }
    }
}
