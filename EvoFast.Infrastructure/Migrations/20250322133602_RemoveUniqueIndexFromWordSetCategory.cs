using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvoFast.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUniqueIndexFromWordSetCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WordSetCategory_WordSetId_CategoryId",
                table: "WordSetCategory");

            migrationBuilder.CreateIndex(
                name: "IX_WordSetCategory_WordSetId",
                table: "WordSetCategory",
                column: "WordSetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WordSetCategory_WordSetId",
                table: "WordSetCategory");

            migrationBuilder.CreateIndex(
                name: "IX_WordSetCategory_WordSetId_CategoryId",
                table: "WordSetCategory",
                columns: new[] { "WordSetId", "CategoryId" },
                unique: true);
        }
    }
}
