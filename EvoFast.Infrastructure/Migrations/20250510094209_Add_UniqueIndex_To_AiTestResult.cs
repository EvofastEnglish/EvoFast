using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvoFast.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_UniqueIndex_To_AiTestResult : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AiTestResults_UserId",
                table: "AiTestResults");

            migrationBuilder.CreateIndex(
                name: "IX_AiTestResults_UserId_AiTestId",
                table: "AiTestResults",
                columns: new[] { "UserId", "AiTestId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AiTestResults_UserId_AiTestId",
                table: "AiTestResults");

            migrationBuilder.CreateIndex(
                name: "IX_AiTestResults_UserId",
                table: "AiTestResults",
                column: "UserId");
        }
    }
}
