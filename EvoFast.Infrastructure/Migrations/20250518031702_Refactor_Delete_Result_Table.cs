using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvoFast.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Refactor_Delete_Result_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AiTestSectionQuestionResult");

            migrationBuilder.DropTable(
                name: "AiTestSectionResult");

            migrationBuilder.DropTable(
                name: "AiTestResults");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AiTestResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AiTestId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ChatPrompt = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    Evaluation = table.Column<string>(type: "text", nullable: false),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AiTestResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AiTestResults_AiTests_AiTestId",
                        column: x => x.AiTestId,
                        principalTable: "AiTests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AiTestResults_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AiTestSectionResult",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AiTestResultId = table.Column<Guid>(type: "uuid", nullable: false),
                    AiTestSectionId = table.Column<Guid>(type: "uuid", nullable: false),
                    ChatPrompt = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    Evaluation = table.Column<string>(type: "text", nullable: false),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    SectionOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AiTestSectionResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AiTestSectionResult_AiTestResults_AiTestResultId",
                        column: x => x.AiTestResultId,
                        principalTable: "AiTestResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AiTestSectionResult_AiTestSections_AiTestSectionId",
                        column: x => x.AiTestSectionId,
                        principalTable: "AiTestSections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AiTestSectionQuestionResult",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AiTestSectionQuestionId = table.Column<Guid>(type: "uuid", nullable: false),
                    AiTestSectionResultId = table.Column<Guid>(type: "uuid", nullable: false),
                    ChatPrompt = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    Evaluation = table.Column<string>(type: "text", nullable: false),
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
                name: "IX_AiTestResults_AiTestId",
                table: "AiTestResults",
                column: "AiTestId");

            migrationBuilder.CreateIndex(
                name: "IX_AiTestResults_UserId_AiTestId",
                table: "AiTestResults",
                columns: new[] { "UserId", "AiTestId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AiTestSectionQuestionResult_AiTestSectionQuestionId_AiTestS~",
                table: "AiTestSectionQuestionResult",
                columns: new[] { "AiTestSectionQuestionId", "AiTestSectionResultId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AiTestSectionQuestionResult_AiTestSectionResultId",
                table: "AiTestSectionQuestionResult",
                column: "AiTestSectionResultId");

            migrationBuilder.CreateIndex(
                name: "IX_AiTestSectionResult_AiTestResultId",
                table: "AiTestSectionResult",
                column: "AiTestResultId");

            migrationBuilder.CreateIndex(
                name: "IX_AiTestSectionResult_AiTestSectionId_AiTestResultId",
                table: "AiTestSectionResult",
                columns: new[] { "AiTestSectionId", "AiTestResultId" },
                unique: true);
        }
    }
}
