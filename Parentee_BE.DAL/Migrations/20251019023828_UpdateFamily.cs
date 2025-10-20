using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parentee_BE.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFamily : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "created_by",
                table: "family",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_family_created_by",
                table: "family",
                column: "created_by");

            migrationBuilder.AddForeignKey(
                name: "FK_family_user_created_by",
                table: "family",
                column: "created_by",
                principalTable: "user",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_family_user_created_by",
                table: "family");

            migrationBuilder.DropIndex(
                name: "IX_family_created_by",
                table: "family");

            migrationBuilder.DropColumn(
                name: "created_by",
                table: "family");
        }
    }
}
