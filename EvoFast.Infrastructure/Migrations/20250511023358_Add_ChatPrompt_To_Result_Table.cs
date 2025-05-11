using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvoFast.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_ChatPrompt_To_Result_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChatPrompt",
                table: "AiTestSectionResult",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ChatPrompt",
                table: "AiTestResults",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChatPrompt",
                table: "AiTestSectionResult");

            migrationBuilder.DropColumn(
                name: "ChatPrompt",
                table: "AiTestResults");
        }
    }
}
