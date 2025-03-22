using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvoFast.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddWordSetCategoryIdToQuestion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "WordSetCategoryId",
                table: "Questions",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Questions_WordSetCategoryId",
                table: "Questions",
                column: "WordSetCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_WordSetCategory_WordSetCategoryId",
                table: "Questions",
                column: "WordSetCategoryId",
                principalTable: "WordSetCategory",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_WordSetCategory_WordSetCategoryId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_WordSetCategoryId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "WordSetCategoryId",
                table: "Questions");
        }
    }
}
