using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parentee_BE.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateALL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "diaper_change_family_id_fkey",
                table: "diaper_change");

            migrationBuilder.DropForeignKey(
                name: "feeding_family_id_fkey",
                table: "feeding");

            migrationBuilder.DropForeignKey(
                name: "sleep_family_id_fkey",
                table: "sleep");

            migrationBuilder.DropForeignKey(
                name: "task_family_id_fkey",
                table: "task");

            migrationBuilder.DropIndex(
                name: "idx_task_family_time",
                table: "task");

            migrationBuilder.DropIndex(
                name: "IX_sleep_family_id",
                table: "sleep");

            migrationBuilder.DropIndex(
                name: "IX_feeding_family_id",
                table: "feeding");

            migrationBuilder.DropIndex(
                name: "IX_diaper_change_family_id",
                table: "diaper_change");

            migrationBuilder.DropColumn(
                name: "family_id",
                table: "task");

            migrationBuilder.DropColumn(
                name: "family_id",
                table: "sleep");

            migrationBuilder.DropColumn(
                name: "family_id",
                table: "feeding");

            migrationBuilder.DropColumn(
                name: "family_id",
                table: "diaper_change");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "family_id",
                table: "task",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "family_id",
                table: "sleep",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "family_id",
                table: "feeding",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "family_id",
                table: "diaper_change",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "idx_task_family_time",
                table: "task",
                columns: new[] { "family_id", "starts_at" });

            migrationBuilder.CreateIndex(
                name: "IX_sleep_family_id",
                table: "sleep",
                column: "family_id");

            migrationBuilder.CreateIndex(
                name: "IX_feeding_family_id",
                table: "feeding",
                column: "family_id");

            migrationBuilder.CreateIndex(
                name: "IX_diaper_change_family_id",
                table: "diaper_change",
                column: "family_id");

            migrationBuilder.AddForeignKey(
                name: "diaper_change_family_id_fkey",
                table: "diaper_change",
                column: "family_id",
                principalTable: "family",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "feeding_family_id_fkey",
                table: "feeding",
                column: "family_id",
                principalTable: "family",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "sleep_family_id_fkey",
                table: "sleep",
                column: "family_id",
                principalTable: "family",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "task_family_id_fkey",
                table: "task",
                column: "family_id",
                principalTable: "family",
                principalColumn: "id");
        }
    }
}
