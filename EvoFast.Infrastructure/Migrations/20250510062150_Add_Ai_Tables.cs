using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvoFast.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_Ai_Tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AiTests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ChatPromptStart = table.Column<string>(type: "text", nullable: false),
                    ChatPromptFinish = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AiTests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AiTestSections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AiTestId = table.Column<Guid>(type: "uuid", nullable: false),
                    SectionOrder = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    TotalQuestion = table.Column<int>(type: "integer", nullable: false),
                    EvaluationCriteria = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ChatPrompt = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AiTestSections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AiTestSections_AiTests_AiTestId",
                        column: x => x.AiTestId,
                        principalTable: "AiTests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AiTestSectionQuestions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AiTestSectionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ThinkingTimeSeconds = table.Column<int>(type: "integer", nullable: false),
                    RecordingTimeSeconds = table.Column<int>(type: "integer", nullable: false),
                    ChatPrompt = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AiTestSectionQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AiTestSectionQuestions_AiTestSections_AiTestSectionId",
                        column: x => x.AiTestSectionId,
                        principalTable: "AiTestSections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AiTestSectionQuestions_AiTestSectionId",
                table: "AiTestSectionQuestions",
                column: "AiTestSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_AiTestSections_AiTestId",
                table: "AiTestSections",
                column: "AiTestId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AiTestSectionQuestions");

            migrationBuilder.DropTable(
                name: "AiTestSections");

            migrationBuilder.DropTable(
                name: "AiTests");
        }
    }
}
