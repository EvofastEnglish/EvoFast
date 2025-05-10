using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvoFast.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AllowNull_Description_Delete_ChatChatPrompt_Columns_AiTestSectionQuestion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChatPrompt",
                table: "AiTestSectionQuestions");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "AiTestSectionQuestions",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "AiTestSectionQuestions",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChatPrompt",
                table: "AiTestSectionQuestions",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
