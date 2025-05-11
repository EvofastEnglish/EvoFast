using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvoFast.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_AiTestSectionQuestionResult_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AiTestSectionQuestionResult",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AiTestSectionResultId = table.Column<Guid>(type: "uuid", nullable: false),
                    AiTestSectionQuestionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Evaluation = table.Column<string>(type: "text", nullable: false),
                    ChatPrompt = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AiTestSectionQuestionResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AiTestSectionQuestionResult_AiTestSectionQuestions_AiTestSe~",
                        column: x => x.AiTestSectionQuestionId,
                        principalTable: "AiTestSectionQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AiTestSectionQuestionResult_AiTestSectionResult_AiTestSecti~",
                        column: x => x.AiTestSectionResultId,
                        principalTable: "AiTestSectionResult",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AiTestSectionQuestionResult_AiTestSectionQuestionId_AiTestS~",
                table: "AiTestSectionQuestionResult",
                columns: new[] { "AiTestSectionQuestionId", "AiTestSectionResultId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AiTestSectionQuestionResult_AiTestSectionResultId",
                table: "AiTestSectionQuestionResult",
                column: "AiTestSectionResultId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AiTestSectionQuestionResult");
        }
    }
}
