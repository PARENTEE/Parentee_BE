using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parentee_BE.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTaskEntity3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "assigned_to",
                table: "task",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_task_assigned_to",
                table: "task",
                column: "assigned_to");

            migrationBuilder.AddForeignKey(
                name: "task_assigned_to_fkey",
                table: "task",
                column: "assigned_to",
                principalTable: "user",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "task_assigned_to_fkey",
                table: "task");

            migrationBuilder.DropIndex(
                name: "IX_task_assigned_to",
                table: "task");

            migrationBuilder.DropColumn(
                name: "assigned_to",
                table: "task");
        }
    }
}
